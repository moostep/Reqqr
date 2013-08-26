using System;
using System.Drawing;
using System.Threading;
using MonoTouch.UIKit;

namespace Reqqr
{
	public class Progress : UIView
	{
		UIActivityIndicatorView activity;

		public Progress()
		{
			BackgroundColor = UIColor.FromWhiteAlpha(0f, 0.75f);
			Frame = UIScreen.MainScreen.Bounds;

			activity = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.WhiteLarge);
			activity.StartAnimating();
			activity.Center = new PointF(Frame.Width / 2, Frame.Height / 2);

			AddSubview(activity);
		}

		static Progress progress;

		public static void Show(bool show)
		{
			UIApplication.SharedApplication.InvokeOnMainThread(() => {

				if (show)
				{
					if (progress == null)
					{
						progress = new Progress();
						UIApplication.SharedApplication.KeyWindow.AddSubview(progress);
					}
				}
				else
				{
					if (progress != null)
					{
						progress.RemoveFromSuperview();
						progress = null;
					}
				}
			});
		}

		public static void ShowForTask(Action action, Action completion = null)
		{
			Show(true);

			ThreadPool.QueueUserWorkItem(delegate {

				try
				{
					action();
				}
				catch(Exception ex)
				{
					Console.WriteLine("EX: " + ex.Message);
				}

				UIApplication.SharedApplication.InvokeOnMainThread(() => {
					Show(false);

					if (completion != null)
						completion();
				});
			});

		}
	}
}