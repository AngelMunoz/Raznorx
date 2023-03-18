namespace Raznorx.Web

open System.Runtime.Versioning
open Avalonia
open Avalonia.Browser

open Raznorx
open Raznorx.Types
open Raznorx.DI
open Raznorx.Services

[<Struct>]
type BrowserEnv =
  interface AppEnv with
    member this.Player: IMusicPlayer = failwith "Not Implemented"

    member this.Songs: ISongProvider = Songs.Impl


module Program =
  [<assembly: SupportedOSPlatform("browser")>]
  do ()

  [<EntryPoint>]
  let main argv =
    AppBuilder.Configure(fun () -> App(BrowserEnv())).SetupBrowserApp("out")
    |> ignore

    0
