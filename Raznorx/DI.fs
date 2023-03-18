namespace Raznorx.DI

open Raznorx.Types
open System.Threading.Tasks

type ISongProvider =
  abstract FromFileSystem: unit -> Task<Song list>

type ISongs =
  abstract Songs: ISongProvider

[<RequireQualifiedAccess>]
module Songs =

  let FromFileSystem (env: #ISongs) = env.Songs.FromFileSystem()


type IMusicPlayer =
  abstract play: Song -> unit
  abstract stop: unit -> unit

type IPlayer =

  abstract Player: IMusicPlayer

[<RequireQualifiedAccess>]
module Player =
  let play (env: #IPlayer) song = env.Player.play song

  let stop (env: #IPlayer) = env.Player.stop ()



/// Main Interface to our application environment
type AppEnv =
  inherit ISongs
  inherit IPlayer
