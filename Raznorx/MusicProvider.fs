[<RequireQualifiedAccess>]
module Raznorx.Services.MusicProvider


open System.Threading.Tasks

open FSharp.Control.Reactive

open Avalonia
open Avalonia.Controls
open Avalonia.Controls.ApplicationLifetimes
open Avalonia.Platform.Storage


open Raznorx.Types
open Raznorx.DI

let private selectFiles (disk: IStorageProvider) = task {
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

let private selectDirectory (disk: IStorageProvider) = task {
  let options = FolderPickerOpenOptions(AllowMultiple = false, Title = "Music Folder")

  let! directories = disk.OpenFolderPickerAsync(options)

  match directories |> Seq.tryHead with
  | Some dir ->
    let dir = System.IO.DirectoryInfo(dir.Path.AbsolutePath)

    return
      dir.EnumerateFiles("*.mp3")
      |> Seq.map (fun file -> {
        name = file.Name
        path = file.FullName
      })
      |> Seq.toList
  | None -> return List.empty
}


let inline private storage<'HasStorage when 'HasStorage: (member StorageProvider: IStorageProvider)>
  (topLevel: 'HasStorage)
  =
  topLevel.StorageProvider


let Impl =
  let defaultPl = { name = "default"; songs = List.empty }
  let library = lazy (ResizeArray<Playlist>([| defaultPl |]))

  let store =
    match Avalonia.Application.Current.ApplicationLifetime with
    | :? IClassicDesktopStyleApplicationLifetime as ds -> ds.MainWindow |> storage |> ValueSome
    | :? ISingleViewApplicationLifetime as sv -> TopLevel.GetTopLevel(sv.MainView) |> storage |> ValueSome
    | _ -> ValueNone

  let sub = Subject.behavior defaultPl

  { new IMusicProvider with
      member _.AddToLibrary(kind: AddResourceKind, ?playlist: string) : Task = task {
        let playlist = defaultArg playlist "default"

        let! files =
          match kind, store with
          | Directory, ValueSome store -> selectDirectory store
          | Files, ValueSome store -> selectFiles store
          | _, ValueNone -> Task.FromResult List.empty<Song>

        match library.Value |> Seq.tryFindIndex (fun lib -> lib.name = playlist) with
        | Some found ->
          library.Value[found] <-
            { library.Value[found] with
                songs = library.Value[found].songs @ files
            }
        | None -> library.Value.Add({ name = playlist; songs = files })

        return ()
      }

      member _.Library: System.Lazy<Song list> =
        lazy (library.Value |> Seq.fold (fun current next -> next.songs @ current) List.empty)

      member _.Playlist: System.IObservable<Playlist> = sub

      member _.Playlists: System.Lazy<Playlist list> = lazy (library.Value |> Seq.toList)

      member _.SelectPlaylist (name: string): unit =
        match library.Value |> Seq.tryFind (fun pl -> pl.name = name) with
        | Some pl -> sub.OnNext pl
        | None -> ()

  }
