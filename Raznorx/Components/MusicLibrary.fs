namespace Raznorx.Components.MusicLibrary


open Avalonia.Controls
open Avalonia.FuncUI
open Avalonia.FuncUI.DSL
open Raznorx.DI

[<RequireQualifiedAccess>]
module Views =
  let private TabView (MusicProvider provider) =
    Component(fun ctx -> DockPanel.create [])

  let Tab (env: #ApplicationEnvironment) =
    TabItem.create [ TabItem.header "Music Library"; TabItem.content (TabView env.MusicProvider) ]
