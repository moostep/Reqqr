using System;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using MonoTouch.UIKit;
using MonoTouch.Dialog;

namespace Reqqr
{
	public class XmlViewController : DialogViewController
	{
		public XmlViewController (XContainer container, string name) : base(null, true)
		{
			Root = new RootElement(name);

			var element = container as XElement;

			if (element != null)
			{
				var attrs = element.Attributes();

				if (attrs.Count() != 0)
				{
					Root.Add(new Section("attributes") {

						from a in attrs select (Element) new StringElement(a.Name.LocalName, a.Value)

					});
				}

				var firstTextNode = element.Nodes().OfType<XText>().FirstOrDefault();
				string text = null;

				if (firstTextNode != null)
				{
					text = firstTextNode.Value;
					if (text != null)
					{
						text = text.Trim();
					}
				}

				if (!String.IsNullOrEmpty(text))
				{
					Root.Add(new Section("text") {

						new StyledMultilineElement("text", text)

					});
				}
			}

			var elements = container.Elements();

			if (elements.Count() != 0)
			{
				Root.Add(new Section("elements") {

					from e in elements select (Element) new XmlRootElement(e.Name.LocalName, re => {
						return new XmlViewController(e, e.Name.LocalName);
					})

				});
			}
		}

		public class XmlRootElement : RootElement
		{
			public XmlRootElement(string caption, Func<RootElement, UIViewController> createOnSelected) : base(caption, createOnSelected)
			{
			}

			public override UITableViewCell GetCell (UITableView tv)
			{
				var cell = base.GetCell (tv);
				cell.SelectionStyle = UITableViewCellSelectionStyle.Gray;
				return cell;
			}
		}
	}
}

