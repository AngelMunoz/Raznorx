namespace Raznorx

open Avalonia
open Avalonia.Controls.ApplicationLifetimes
open Avalonia.FuncUI.Hosts
open Avalonia.Themes.Fluent
open Raznorx.DI
open Raznorx.Views


type MainWindow(env: AppEnv) as this =
  inherit HostWindow()

  do
    base.Title <- "BasicTemplate"
    base.Width <- 400.0
    base.Height <- 400.0
    this.Content <- MainView.view (env)

type App(env: AppEnv) =
  inherit Application()

  override this.Initialize() = this.Styles.Add(FluentTheme())

  override this.OnFrameworkInitializationCompleted() =
    match this.ApplicationLifetime with
    | :? IClassicDesktopStyleApplicationLifetime as desktopLifetime -> desktopLifetime.MainWindow <- MainWindow(env)
    | :? ISingleViewApplicationLifetime as singleViewLifetime -> singleViewLifetime.MainView <- MainView.view (env)
    | _ -> ()
