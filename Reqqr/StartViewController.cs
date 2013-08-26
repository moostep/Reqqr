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
					new StringElement("Menu Item 1", MenuItemHandler1),
					new StringElement("Menu Item 2"),
					new StringElement("Menu Item 3"),
				},
				new Section {
					new StringElement("About", ShowAbout)
				}
			};
		}

		void MenuItemHandler1()
		{
			Console.WriteLine("Menu Item 1 tapped");
		}

		void ShowAbout()
		{
			var vc = new AboutViewController();
			this.NavigationController.PushViewController(vc, true);
		}
	}
}

