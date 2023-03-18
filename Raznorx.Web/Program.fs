open System.Runtime.Versioning
open Avalonia
open Avalonia.Browser

open AvaloniaTest

open Raznorx.Types
open Raznorx.DI


[<Struct>]
type BrowserEnv =
  interface AppEnv with
    member this.Player: IMusicPlayer = failwith "Not Implemented"

    member this.Songs: ISongProvider = failwith "Not Implemented"


module Program =
  [<assembly: SupportedOSPlatform("browser")>]
  do ()

  [<EntryPoint>]
  let main argv =
    AppBuilder.Configure(fun () -> App(BrowserEnv())).SetupBrowserApp("out")
    |> ignore

    0
