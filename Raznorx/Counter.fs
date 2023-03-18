namespace Raznorx

module Counter =
    open Avalonia.Controls
    open Avalonia.FuncUI
    open Avalonia.FuncUI.DSL
    open Avalonia.Layout
    open Raznorx.DI
    open Raznorx.Types

    let songTemplate song =
        TextBlock.create [ TextBlock.text $"{song.name}: - {song.path}" ]

    let view (env: #AppEnv) =

        Component(fun ctx ->
            let songs = ctx.useState List.empty

            StackPanel.create
                [ StackPanel.verticalAlignment VerticalAlignment.Center
                  StackPanel.horizontalAlignment HorizontalAlignment.Center
                  StackPanel.children
                      [ ListBox.create
                            [ ListBox.dataItems songs.Current
                              ListBox.itemTemplate (DataTemplateView.create<_, _> (songTemplate)) ]

                        Button.create
                            [ Button.content "Select Music Files"
                              Button.onClick (fun _ ->
                                  async {
                                      let! selectedSongs = env.Songs.FromFileSystem() |> Async.AwaitTask
                                      songs.Set(selectedSongs)
                                  }
                                  |> Async.Start) ] ] ])
