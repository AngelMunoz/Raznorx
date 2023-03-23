namespace Raznorx.Components.Playlist


open Avalonia.Controls
open Avalonia.FuncUI
open Avalonia.FuncUI.DSL
open Raznorx.DI

[<RequireQualifiedAccess>]
module Views =

  let TabView (MusicProvider provider) =
    Component(fun ctx -> DockPanel.create [])

  let Tab (env: #ApplicationEnvironment) =

    TabItem.create [ TabItem.header "Playlist"; TabItem.content (TabView env.MusicProvider) ]
