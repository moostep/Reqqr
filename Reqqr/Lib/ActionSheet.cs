using System;
using System.Collections.Generic;
using System.Linq;

using MonoTouch.UIKit;

namespace Reqqr
{
	public class ActionSheet
	{
		readonly string sheetTitle;
		readonly List<Button> otherButtons;

		Button cancelButton;
		Button destroyButton;

		UIActionSheet sheet;

		public static ActionSheet Begin(string title)
		{
			return new ActionSheet(title);
		}

		ActionSheet(string title)
		{
			sheetTitle = title;
			otherButtons = new List<Button>();
		}

		public ActionSheet AddCancel(string title, Action handler = null)
		{
			cancelButton = new Button { Title = title, Handler = handler };
			return this;
		}

		public ActionSheet AddDestroy(string title, Action handler = null)
		{
			destroyButton = new Button { Title = title, Handler = handler };
			return this;
		}

		public ActionSheet AddOther(string title, Action handler = null)
		{
			otherButtons.Add(new Button { Title = title, Handler = handler });
			return this;
		}

		public UIActionSheet End()
		{
			var other = from b in otherButtons select b.Title;

			sheet = new UIActionSheet(
				sheetTitle,
				null,
				(cancelButton == null) ? null : cancelButton.Title,
				(destroyButton == null) ? null : destroyButton.Title,
				other.ToArray());

			sheet.Clicked += SheetClickedHandler;

			return sheet;
		}

		void SheetClickedHandler(object sender, UIButtonEventArgs e)
		{
			var buttonIndex = e.ButtonIndex;

			if (destroyButton != null)
				buttonIndex--;

			if (buttonIndex < 0)
			{
				if (destroyButton != null && destroyButton.Handler != null)
					destroyButton.Handler();

				return;
			}

			if (buttonIndex < otherButtons.Count)
			{
				var o = otherButtons[buttonIndex];

				if (o.Handler != null)
					o.Handler();

				return;
			}

			if (cancelButton != null && cancelButton.Handler != null)
				cancelButton.Handler();
		}
	}

	class Button
	{
		public string Title;
		public Action Handler;
	}
}
