namespace Raznorx

open Avalonia
open Avalonia.Controls.ApplicationLifetimes
open Avalonia.FuncUI.Hosts
open Avalonia.Styling
open Avalonia.Themes.Fluent
open Raznorx.DI
open Raznorx.Views

type App(env: ApplicationEnvironment) =
  inherit Application()
  do
    base.RequestedThemeVariant <- ThemeVariant.Dark

  override this.Initialize() =
      this.Styles.Add(FluentTheme())

  override this.OnFrameworkInitializationCompleted() =
    match this.ApplicationLifetime with
    | :? IClassicDesktopStyleApplicationLifetime as desktopLifetime ->
      desktopLifetime.MainWindow <- HostWindow(Content = MainView.view env)
    | :? ISingleViewApplicationLifetime as singleViewLifetime -> singleViewLifetime.MainView <- MainView.view env
    | _ -> ()
