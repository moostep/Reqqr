using System;
using MonoTouch.Dialog;

namespace Reqqr
{
	public class AboutViewController : DialogViewController
	{
		public AboutViewController () : base(null, true)
		{
			Root = new RootElement("About") {
				new Section {
					new StringElement("Version", "0.1"),
				}
			};
		}
	}
}

