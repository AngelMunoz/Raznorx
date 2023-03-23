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
  interface ApplicationEnvironment with
    member _.MusicPlayer: IMusicPlayer = failwith "Not Implemented"

    member _.MusicProvider: IMusicProvider = MusicProvider.Impl


module Program =
  [<assembly: SupportedOSPlatform("browser")>]
  do ()

  [<EntryPoint>]
  let main argv =
    AppBuilder.Configure(fun () -> App(BrowserEnv())).SetupBrowserApp("out")
    |> ignore

    0
