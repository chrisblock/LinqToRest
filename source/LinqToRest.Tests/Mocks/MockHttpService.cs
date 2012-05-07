using System;
using System.Collections.Generic;
using System.Linq;

using LinqToRest.Http;
using LinqToRest.OData;

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
				TestProperty = "4"
			},
			new TestObject
			{
				TestProperty = "3"
			},
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

			var parser = new ODataUriParser();

			var lambda = parser.Parse(typeof (TestObject), new Uri(url));

			var fn = lambda.Compile();

			var result = fn.DynamicInvoke(Result.AsQueryable());

			return JsonConvert.SerializeObject(result);
		}

		public string Get(Uri uri)
		{
			return Get(uri.ToString());
		}
	}
}
