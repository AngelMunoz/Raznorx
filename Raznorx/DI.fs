namespace Raznorx.DI

open System
open Raznorx.Types
open System.Threading.Tasks
open Avalonia.Platform.Storage


type AddResourceKind =
  | Directory
  | Files

// Service Definitions

type IMusicProvider =

  /// Provides the current playlist selected by the user
  abstract Playlist: IObservable<Playlist>
  /// The whole music added to the current library
  abstract Library: Lazy<Song list>
  /// Adds a particular set of songs or a the songs in a directory
  /// to the music library
  abstract AddToLibrary: kind: AddResourceKind * ?playlist: string -> Task

  abstract Playlists: Lazy<Playlist list>

  abstract SelectPlaylist: name: string -> unit


type IMusicPlayer =
  abstract play: Song -> unit
  abstract stop: unit -> unit

[<AutoOpen>]
module Patterns =
  let (|MusicProvider|) (provider: #IMusicProvider) =
    MusicProvider(provider :> IMusicProvider)

  let (|MusicPlayer|) (provider: #IMusicPlayer) = MusicPlayer(provider :> IMusicPlayer)

  let (|DiskStorageProvider|) (provider: #IStorageProvider) =
    DiskStorageProvider(provider :> IStorageProvider)


/// Application Environment: This is the interface that acts as the root of the Environment DI System
/// For each service we use a parent property that acts as the container for that service
/// in the case of the IMusicPlayer service we use "Player" as the property thrp
type ApplicationEnvironment =

  abstract MusicProvider: IMusicProvider
  abstract MusicPlayer: IMusicPlayer


// Module Implementations

[<RequireQualifiedAccess>]
module Songs =

  let Playlist (MusicProvider provider) = provider.Playlist

  let Library (MusicProvider provider) = provider.Library.Value

  let AddToPlaylist (MusicProvider provider) playlist kind = provider.AddToLibrary(kind, playlist)

  let AddToLibrary (MusicProvider provider) = AddToPlaylist provider "default"



[<RequireQualifiedAccess>]
module Player =
  let play (MusicPlayer player) song = player.play song

  let stop (MusicPlayer player) = player.stop ()
