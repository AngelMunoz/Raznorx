namespace Raznorx.Desktop

open System
open Avalonia
open Raznorx
open Raznorx.DI
open Raznorx.Services

[<Struct>]
type DesktopEnv =
  
  interface ApplicationEnvironment with
    member _.MusicPlayer: IMusicPlayer = failwith "Not Implemented"

    member _.MusicProvider: IMusicProvider = MusicProvider.Impl

module Program =
  [<EntryPoint; STAThread>]
  let main argv =
    AppBuilder
      .Configure(fun () -> App(DesktopEnv()))
      .UsePlatformDetect()
      .StartWithClassicDesktopLifetime(argv)
    |> ignore

    0
