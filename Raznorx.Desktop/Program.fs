namespace Raznorx.Desktop

open System
open Avalonia
open Raznorx
open Raznorx.DI

[<Struct>]
type DesktopEnv =
  interface AppEnv with
    member _.Player: IMusicPlayer = failwith "Not Implemented"

    member _.Songs: ISongProvider = Songs.Impl

module Program =
  [<EntryPoint; STAThread>]
  let main argv =
    AppBuilder
      .Configure(fun () -> App(DesktopEnv()))
      .UsePlatformDetect()
      .StartWithClassicDesktopLifetime(argv)
    |> ignore

    0
