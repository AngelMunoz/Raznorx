namespace Raznorx.Android

open Android.App
open Android.Content
open Android.Content.PM


type Application = Android.App.Application

open Avalonia
open Avalonia.Android

open Raznorx
open Raznorx.DI

[<Struct>]
type AndroidEnv =
  interface AppEnv with
    member _.Player: IMusicPlayer = failwith "Not Implemented"

    member _.Songs: ISongProvider = Songs.Impl


[<Activity(Label = "Raznorx.Android",
           Theme = "@style/MyTheme.NoActionBar",
           Icon = "@drawable/icon",
           LaunchMode = LaunchMode.SingleInstance,
           ConfigurationChanges = (ConfigChanges.Orientation ||| ConfigChanges.ScreenSize))>]
type MainActivity() =
  inherit AvaloniaMainActivity()


[<Activity(Theme = "@style/MyTheme.Splash", MainLauncher = true, NoHistory = true)>]
type SplashActivity() =
  inherit AvaloniaSplashActivity()

  override _.CreateAppBuilder() = AppBuilder.Configure(fun () -> App(AndroidEnv())).UseAndroid()

  override x.OnResume() =
    base.OnResume()
    x.StartActivity(new Intent(Application.Context, typeof<MainActivity>))
