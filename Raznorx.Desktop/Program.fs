namespace Raznorx.Desktop
open System
open Avalonia
open Raznorx

module Program =
    let BuildAvaloniaApp() =
        AppBuilder
            .Configure<App>()
            .UsePlatformDetect()


    [<EntryPoint; STAThread>]
    let main argv =
        BuildAvaloniaApp().StartWithClassicDesktopLifetime(argv) |> ignore
        0
