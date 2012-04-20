using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

using Newtonsoft.Json;

using Remotion.Linq;

namespace LinqToRest
{
	public class RestQueryExecutor : IQueryExecutor
	{
		private readonly RestQueryModelVisitor _visitor = new RestQueryModelVisitor();

		private static string GetJsonResult(Uri uri)
		{
			var json = String.Empty;

			var request = (HttpWebRequest) WebRequest.Create(uri);
			request.Accept = "application/json";

			var response = (HttpWebResponse) request.GetResponse();

			// TODO: check for other status codes here
			if (response.StatusCode == HttpStatusCode.OK)
			{
				using (var stream = response.GetResponseStream())
				{
					if (stream != null)
					{
						using (var reader = new StreamReader(stream))
						{
							json = reader.ReadToEnd();
						}
					}
				}
			}

			return json;
		}

		public T ExecuteScalar<T>(QueryModel queryModel)
		{
			var url = _visitor.Translate(queryModel);

			var uri = new Uri(url);

			var json = GetJsonResult(uri);

			return JsonConvert.DeserializeObject<T>(json);
		}

		public T ExecuteSingle<T>(QueryModel queryModel, bool returnDefaultWhenEmpty)
		{
			var url = _visitor.Translate(queryModel);

			var uri = new Uri(url);

			var json = GetJsonResult(uri);

			var result = default(T);

			if ((returnDefaultWhenEmpty == false) && String.IsNullOrWhiteSpace(json))
			{
				throw new InvalidOperationException("Sequence contains no elements.");
			}
			else if (String.IsNullOrWhiteSpace(json) == false)
			{
				// TODO: deserialize an array and check the length???
				result = JsonConvert.DeserializeObject<T>(json);
			}

			return result;
		}

		public IEnumerable<T> ExecuteCollection<T>(QueryModel queryModel)
		{
			var url = _visitor.Translate(queryModel);

			var uri = new Uri(url);

			var json = GetJsonResult(uri);

			return JsonConvert.DeserializeObject<IEnumerable<T>>(json);
		}
	}
}
