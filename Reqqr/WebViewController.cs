using System;
using MonoTouch.Dialog;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.Net;

namespace Reqqr
{
	public class WebViewController : UIViewController
	{
		UIWebView webView;
		Response response;

		public WebViewController (Response response)
		{
			this.response = response;
			Title = "Response html";
		}

		public override void ViewWillLayoutSubviews ()
		{
			base.ViewWillLayoutSubviews ();
			webView.Frame = View.Bounds;
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			webView = new UIWebView ();
			View.AddSubview (webView);
			webView.LoadHtmlString(response.Body, null);
		}
	}
}