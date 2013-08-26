using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.Dialog;

namespace Reqqr
{
	public partial class RequestDetailViewController : DialogViewController
	{
		Request request;

		public RequestDetailViewController (Request request) : base (null, true)
		{
			this.request = request;
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			Root = new RootElement ("Request detail") {
				new Section (){
					new StringElement ("Name", request.Name),
					new StyledMultilineElement ("Url", request.Url),
				},
			};
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			NavigationItem.RightBarButtonItem = new UIBarButtonItem(UIBarButtonSystemItem.Action, delegate {
				ActionSheet.Begin("Actions")
						.AddCancel("Cancel")
						.AddOther ("Edit request", EditRequest)
						.AddOther ("Post request", PostRequest)
						.End()
						.ShowFrom(NavigationItem.RightBarButtonItem, true);
			});
		}

		void EditRequest()
		{
			var vc = new RequestEditViewController(request);
			NavigationController.PushViewController(vc, true);
		}

		void PostRequest() 
		{
			var response = new Response ();
			Progress.ShowForTask (
				() => { 
				using (var client = new System.Net.WebClient()) {
					response.Body = client.DownloadString (request.Url);
				}
			}, () => {
				var vc = new ResponseViewController(response);
				NavigationController.PushViewController(vc, true);
			});

		}
	}
}
