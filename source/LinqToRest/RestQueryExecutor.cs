using System;
using System.Collections.Generic;

using Newtonsoft.Json;

using Remotion.Linq;

namespace LinqToRest
{
	public class RestQueryExecutor : IQueryExecutor
	{
		private readonly Uri _url;
		private readonly RestQueryModelVisitor _visitor = new RestQueryModelVisitor();

		private static string GetJsonResult(Uri uri)
		{
			// TODO: make Web API request
			return String.Empty;
		}

		public RestQueryExecutor(string url)
		{
			_url = new Uri(url);
		}

		public T ExecuteScalar<T>(QueryModel queryModel)
		{
			var queryString = _visitor.Translate(queryModel);

			var uri = new Uri(_url, queryString);

			var json = GetJsonResult(uri);

			return JsonConvert.DeserializeObject<T>(json);
		}

		public T ExecuteSingle<T>(QueryModel queryModel, bool returnDefaultWhenEmpty)
		{
			var queryString = _visitor.Translate(queryModel);

			var uri = new Uri(_url, queryString);

			var json = GetJsonResult(uri);

			var result = default(T);

			if ((returnDefaultWhenEmpty == false) && String.IsNullOrWhiteSpace(json))
			{
				throw new InvalidOperationException("Sequence contains no elements.");
			}
			else if (String.IsNullOrWhiteSpace(json) == false)
			{
				result = JsonConvert.DeserializeObject<T>(json);
			}

			return result;
		}

		public IEnumerable<T> ExecuteCollection<T>(QueryModel queryModel)
		{
			var queryString = _visitor.Translate(queryModel);

			var uri = new Uri(_url, queryString);

			var json = GetJsonResult(uri);

			return JsonConvert.DeserializeObject<IEnumerable<T>>(json);
		}
	}
}
