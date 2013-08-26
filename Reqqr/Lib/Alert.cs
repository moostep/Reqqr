using System;
using MonoTouch.UIKit;

namespace Reqqr
{
	public static class Alert
	{
		public static void Show(string message)
		{
			UIApplication.SharedApplication.InvokeOnMainThread(() => {
				new UIAlertView("", message, null, null, "OK").Show();
			});
		}

		public static void Show(string message, string button1, string button2, Action<int> handler)
		{
			UIApplication.SharedApplication.InvokeOnMainThread(() => {
				var alert = new UIAlertView("", message, null, button1, button2);
				alert.Clicked += (object sender, UIButtonEventArgs e) => {
					handler(e.ButtonIndex);
				};

				alert.Show();
			});
		}

		public static void AreYouSure(Action yes = null, Action no = null, string message = "Are you sure?")
		{
			Show(message, "No", "Yes", buttonIndex => {

				if (buttonIndex == 0)
				{
					if (no != null) no();
				}
				else
				{
					if (yes != null) yes();
				}
			});
		}
	}
}
