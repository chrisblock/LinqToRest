using System;
using System.Collections.Generic;

using LinqToRest.Http;

using Newtonsoft.Json;

namespace LinqToRest.Tests.Mocks
{
	public class MockHttpService : IHttpService
	{
		public static readonly Stack<string> RequestedUrls = new Stack<string>();
		private static readonly object Locker = new object();

		public static readonly IEnumerable<TestObject> Result = new List<TestObject>
		{
			new TestObject
			{
				TestProperty = "1"
			},
			new TestObject
			{
				TestProperty = "2"
			}
		};

		public string Get(string url)
		{
			lock (Locker)
			{
				RequestedUrls.Push(url);
			}

			return JsonConvert.SerializeObject(Result);
		}

		public string Get(Uri uri)
		{
			return Get(uri.ToString());
		}
	}
}
