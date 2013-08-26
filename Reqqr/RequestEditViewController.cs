using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.Dialog;

namespace Reqqr
{
	public partial class RequestEditViewController : DialogViewController
	{
		Request request;

		EntryElement name;
		EntryElement url;

		public RequestEditViewController (Request request) : base (null, true)
		{
			this.request = request;

			name = new EntryElement("Name", "Enter name", request.Name);
			url = new EntryElement("Url", "Enter url", request.Url);

			Root = new RootElement ("Request edit") {
				new Section {
					name,
					url,
				},
			};
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			NavigationItem.RightBarButtonItem = new UIBarButtonItem(UIBarButtonSystemItem.Save, delegate {
				Save();
			});
		}

		void Save()
		{
			request.Name = name.Value;
			request.Url = url.Value;
			NavigationController.PopViewControllerAnimated(true);
		}
	}
}
