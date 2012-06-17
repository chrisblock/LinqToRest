using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

using LinqToRest.Client.Http;
using LinqToRest.Server.OData;

using Newtonsoft.Json;

namespace LinqToRest.Client.Tests.Mocks
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

		public void Delete(string url)
		{
			throw new NotImplementedException();
		}

		public void Delete(Uri uri)
		{
			throw new NotImplementedException();
		}

		public void Post(Uri uri, HttpContent content)
		{
			throw new NotImplementedException();
		}

		public void Post(string url, HttpContent content)
		{
			throw new NotImplementedException();
		}

		public void Put(Uri uri, HttpContent content)
		{
			throw new NotImplementedException();
		}

		public void Put(string url, HttpContent content)
		{
			throw new NotImplementedException();
		}
	}
}
