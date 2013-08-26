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
					new StringElement("Alert demo", AlertDemoHandler),
					new StringElement("Progress demo", ProgressDemoHandler),
					new StringElement("WebClient demo", WebClientDemoHandler),
				},
				new Section {
					new StringElement("About", ShowAbout)
				}
			};
		}

		void AlertDemoHandler()
		{
			Alert.AreYouSure(() => {
				Alert.Show("yes tapped");
			});
		}

		void ProgressDemoHandler()
		{
			Progress.ShowForTask(
				() => {
				System.Threading.Thread.Sleep(2000);
			},
			() => {
				Alert.Show("finished long task");
			});
		}

		void WebClientDemoHandler()
		{
			var response = String.Empty;

			Progress.ShowForTask(
				() => {
				using (var client = new System.Net.WebClient())
				{
					//var url = "http://api.openweathermap.org/data/2.5/weather?q=Munich&mode=json";
					var url = "http://api.openweathermap.org/data/2.5/weather?q=Munich&mode=xml";
					response = client.DownloadString(url);
				}
			},
			() => {
				Alert.Show(response);
			});
		}

		void ShowAbout()
		{
			var vc = new AboutViewController();
			this.NavigationController.PushViewController(vc, true);
		}
	}
}

