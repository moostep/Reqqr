using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Reqqr
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		UIWindow window;

		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			window = new UIWindow (UIScreen.MainScreen.Bounds);

			var vc = new StartViewController();
			var nav = new UINavigationController(vc);

			window.RootViewController = nav;
			window.MakeKeyAndVisible ();
			
			return true;
		}
	}
}

