using System;
using System.Linq;
using System.Json;
using MonoTouch.Dialog;

namespace Reqqr
{
	public class JsonViewController : DialogViewController
	{
		public JsonViewController (JsonObject obj, string title): base(null, true)
		{
			var keys = from key in obj.Keys select key;

			Root = new RootElement(title) {
				new Section("") {
					from key in keys select CreateJsonElement(key, obj[key])
				}
			};
		}

		public JsonViewController (JsonArray arr, string title): base(null, true)
		{
			Root = new RootElement(title) {
				new Section("") {
					arr.Select((item, index) => CreateJsonElement(string.Format("item-{0}", index), item))
				}
			};
		}

		private Element CreateJsonElement(string key, JsonValue value)
		{
			if (value == null)
				return new StringElement(key, "");

			if (value.JsonType == JsonType.Boolean)
			{
				return new StringElement(key, value.ToString());
			}

			if (value.JsonType == JsonType.Number)
			{
				return new StringElement(key, value.ToString());
			}

			if (value.JsonType == JsonType.String)
			{
				return new StringElement(key, value.ToString());
			}

			if (value.JsonType == JsonType.Object)
			{
				var objectElement = new StringElement (key, "Object");

				objectElement.Tapped += () => {
					var jvc = new JsonViewController(value as JsonObject, key);
					NavigationController.PushViewController(jvc, true);
				};

				return objectElement;
			}

			if (value.JsonType == JsonType.Array) 
			{
				var objectElement = new StringElement (key, "Array");

				objectElement.Tapped += () => {
					var jvc = new JsonViewController (value as JsonArray, key);
					NavigationController.PushViewController (jvc, true);
				};

				return objectElement;

			}

			return new StringElement(key, value.JsonType.ToString());
		}
	}
}

