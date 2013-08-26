using System;
using MonoTouch.Dialog;

namespace Reqqr
{
	public class StartViewController : DialogViewController
	{
		public StartViewController () : base(null, true)
		{
			Root = new RootElement("Reqqr") {
				new Section {
					new StringElement("Menu Item 1"),
					new StringElement("Menu Item 2"),
					new StringElement("Menu Item 3"),
				}
			};
		}
	}
}

