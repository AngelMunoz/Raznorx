module Raznorx.Views.MainView

open Avalonia.Controls
open Avalonia.FuncUI
open Avalonia.FuncUI.DSL
open Avalonia.Layout
open Raznorx.DI
open Raznorx.Types
open Raznorx.Components

let view (env: ApplicationEnvironment) =

  Component(fun ctx ->

    TabControl.create [ TabControl.viewItems [ MusicLibrary.Views.Tab env; Playlist.Views.Tab env ] ])
