namespace Raznorx.iOS;


using System;
using Foundation;
using UIKit;
using Avalonia;
using Avalonia.Controls;
using Avalonia.iOS;
using Avalonia.Media;
using Raznorx.DI;


public struct IosEnv: ApplicationEnvironment
{
    public IMusicPlayer MusicPlayer => throw new NotImplementedException("Not Implemented");
    public IMusicProvider MusicProvider => Services.MusicProvider.Impl;
}

public class IosApp: App
{
    public IosApp() : base(new IosEnv()) {}
}

// The UIApplicationDelegate for the application. This class is responsible for launching the 
// User Interface of the application, as well as listening (and optionally responding) to 
// application events from iOS.
[Register("AppDelegate")]
public partial class AppDelegate : AvaloniaAppDelegate<IosApp>
{
}
