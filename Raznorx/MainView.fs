module Raznorx.Views.MainView

open Avalonia.Controls
open Avalonia.FuncUI
open Avalonia.FuncUI.DSL
open Avalonia.Layout
open Raznorx.DI
open Raznorx.Types

let private songTemplate song =
  TextBlock.create [ TextBlock.text $"{song.name}: - {song.path}" ]

let private collectSongs (env: ISongProvider, songs: IWritable<Song list>) =
  async {
    let! selectedSongs = env.FromFileSystem() |> Async.AwaitTask
    songs.Set(selectedSongs)
  }
  |> Async.Start

let view (env: #AppEnv) =

  Component(fun ctx ->
    let songs = ctx.useState List.empty

    DockPanel.create [
      DockPanel.horizontalAlignment HorizontalAlignment.Stretch
      DockPanel.children [
        Button.create [
          Button.dock Dock.Top
          Button.content "Select Music Files"
          Button.onClick (fun _ -> collectSongs (env.Songs, songs))
        ]
        ScrollViewer.create [
          ScrollViewer.dock Dock.Bottom
          ScrollViewer.content (
            ListBox.create [
              ListBox.dataItems songs.Current
              ListBox.itemTemplate (DataTemplateView.create<_, _> songTemplate)
            ]
          )
        ]
      ]
    ])
