using System;
using System.Linq;
using MonoTouch.Dialog;

namespace Reqqr
{
	public class RequestListViewController : DialogViewController
	{
		static RequestList requests;

		public RequestListViewController () : base(null, true)
		{
			if (requests == null)
			{
				requests = RequestList.CreateDemo();
			}
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			Root = new RootElement("Requests") {
				new Section {
					from request in requests select CreateElement(request)
				}
			};
		}

		Element CreateElement(Request request)
		{
			var element = new StringElement(request.Name, () => {
				var vc = new RequestDetailViewController(request);
				NavigationController.PushViewController(vc, true);
			});

			return element;
		}
	}
}

