using System;
using Raznorx.DI;

namespace Raznorx.iOS;

using Foundation;
using UIKit;
using Avalonia;
using Avalonia.Controls;
using Avalonia.iOS;
using Avalonia.Media;

public struct IosEnv: AppEnv
{
    public IMusicPlayer Player => throw new NotImplementedException("Not Implemented");
    public ISongProvider Songs => throw new NotImplementedException("Not Implemented");
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
