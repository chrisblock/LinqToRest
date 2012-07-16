using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;

using Changes;

using DataModel.Tests;

using LinqToRest.Client.Http;
using LinqToRest.Server.OData;
using LinqToRest.Server.OData.Expressions;
using LinqToRest.Server.OData.Expressions.Impl;
using LinqToRest.Server.OData.Parsing;
using LinqToRest.Server.OData.Parsing.Impl;

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

		public T Get<T>(string url)
		{
			lock (Locker)
			{
				RequestedUrls.Push(url);
			}

			var parser = new ODataUriParser(new ODataQueryParser(new ODataQueryPartParserStrategy()), new ExpressionODataQueryVisitor(new FilterExpressionBuilder()));

			var lambda = parser.Parse(typeof (TestObject), new Uri(url));

			var fn = lambda.Compile();

			var result = fn.DynamicInvoke(Result.AsQueryable());

			var json = JsonConvert.SerializeObject(result);

			return JsonConvert.DeserializeObject<T>(json);
		}

		public T Get<T>(Uri uri)
		{
			return Get<T>(uri.ToString());
		}

		public HttpStatusCode Put<T>(Uri uri, ChangeSet<T> changes)
		{
			throw new NotImplementedException();
		}

		public HttpStatusCode Put<T>(string url, ChangeSet<T> changes)
		{
			throw new NotImplementedException();
		}

		public HttpStatusCode Post<T>(string url, T item)
		{
			throw new NotImplementedException();
		}

		public HttpStatusCode Post<T>(Uri uri, T item)
		{
			throw new NotImplementedException();
		}

		public HttpStatusCode Delete(string url)
		{
			throw new NotImplementedException();
		}

		public HttpStatusCode Delete(Uri uri)
		{
			throw new NotImplementedException();
		}
	}
}
