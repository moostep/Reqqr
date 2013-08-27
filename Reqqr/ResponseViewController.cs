using System;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Linq;
using System.IO;
using System.Text;
using System.Json;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.Dialog;

namespace Reqqr
{
	public partial class ResponseViewController : UIViewController
	{
		Response response;
		UITextView tv;

		public ResponseViewController (Response response)
		{
			this.response = response;

			tv = new UITextView
			{
				Text = response.Body,
				Font = UIFont.FromName ("Courier", 18),
				Editable = false,
			};

			View.Add (tv);
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			NavigationItem.RightBarButtonItem = new UIBarButtonItem(UIBarButtonSystemItem.Action, delegate {
				ActionSheet.Begin ("Type")
					.AddCancel ("Cancel")
						.AddOther ("Show as JSON", ShowJson)
						.AddOther ("Show as XML", ShowXml)
						.AddOther ("Show response", ShowResponse)
						.End ()
						.ShowFrom(NavigationItem.RightBarButtonItem, true);
			});
		}

		public override void ViewWillLayoutSubviews ()
		{
			base.ViewWillLayoutSubviews ();
			tv.Frame = View.Bounds;
		}

		void ShowJson()
		{
			try
			{
				var jsonObject = CreateJson (response.Body);

				var jvc = new JsonViewController (jsonObject, "Headers");
				NavigationController.PushViewController(jvc, true);
			}
			catch (Exception ex)
			{
				Alert.Show ("JSON parsing failed"+ ex.Message);
			}
		}

		void ShowXml()
		{
			try
			{
				var doc = XDocument.Parse (response.Body);

				var vc = new XmlViewController(doc, "XML View");
				NavigationController.PushViewController(vc, true);
			}
			catch(Exception ex)
			{
				Alert.Show ("XML parsing failed: " + ex.Message);
			}
		}

		private JsonObject CreateJson(string json)
		{
			using (var reader = new StringReader(json))
			{
				return JsonValue.Load(reader) as JsonObject;
			}
		}

		void ShowResponse ()
		{
			var vc = new WebViewController(response);
			NavigationController.PushViewController(vc, true);
		}
	}
}
