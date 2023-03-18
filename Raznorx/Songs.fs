[<RequireQualifiedAccess>]
module Raznorx.Services.Songs


open System.Threading.Tasks

open Avalonia
open Avalonia.Controls
open Avalonia.Controls.ApplicationLifetimes
open Avalonia.Platform.Storage


open Raznorx.Types
open Raznorx.DI

let private pickSongs (disk: IStorageProvider) = task {
  let options =
    FilePickerOpenOptions(
      AllowMultiple = true,
      FileTypeFilter = [ FilePickerFileType("Song Files", Patterns = [ "*.mp3" ]) ]
    )

  let! files = disk.OpenFilePickerAsync(options)

  return
    files
    |> Seq.map (fun file -> {
      name = file.Name
      path = file.Path.AbsolutePath
    })
    |> Seq.toList
}

let Impl =
  { new ISongProvider with
      member _.FromFileSystem() : Task<Song list> =
        match Application.Current.ApplicationLifetime with
        | :? IClassicDesktopStyleApplicationLifetime as desktop -> pickSongs desktop.MainWindow.StorageProvider
        | :? ISingleViewApplicationLifetime as view -> pickSongs (TopLevel.GetTopLevel(view.MainView).StorageProvider)
        | _ -> Task.FromResult List.empty
  }
