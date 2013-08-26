using System;
using System.Collections.Generic;

namespace Reqqr
{
	public class Request
	{
		public string Url;
		public string Name;
	}

	public class RequestList : List<Request>
	{
		public static RequestList CreateDemo()
		{
			return new RequestList {
				new Request { Name = "Google", Url = "http://www.google.com" },
				new Request { Name = "Yahoo", Url = "http://www.yahoo.com" },
				new Request { Name = "Weather JSON", Url = "http://api.openweathermap.org/data/2.5/weather?q=Munich&mode=json"},
				new Request { Name = "Weather XML", Url = "http://api.openweathermap.org/data/2.5/weather?q=Munich&mode=xml"},
			};
		}
	}
}

