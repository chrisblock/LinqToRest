using System;
using System.Collections.Generic;
using System.Linq;

using LinqToRest.Client.Http;

using Remotion.Linq;

namespace LinqToRest.Client.Linq
{
	public class RestQueryExecutor : IQueryExecutor
	{
		private readonly IQueryModelTranslator _queryModelTranslator;

		private readonly IHttpService _httpService;

		public RestQueryExecutor(IQueryModelTranslator queryModelTranslator, IHttpService httpService)
		{
			_queryModelTranslator = queryModelTranslator;
			_httpService = httpService;
		}

		private Uri GetUri(QueryModel queryModel)
		{
			var url = _queryModelTranslator.Translate(queryModel);

			var uri = new Uri(url);

			return uri;
		}

		private T Execute<T>(QueryModel queryModel)
		{
			var uri = GetUri(queryModel);

			var result = _httpService.Get<T>(uri);

			return result;
		}

		//private object ExecuteInlineCount(QueryModel queryModel)
		//{
		//    throw new NotImplementedException();
		//}

		public T ExecuteScalar<T>(QueryModel queryModel)
		{
			return Execute<T>(queryModel);
		}

		public T ExecuteSingle<T>(QueryModel queryModel, bool returnDefaultWhenEmpty)
		{
			//var resultType = typeof (T);

			//if (resultType.IsGenericType && (resultType.GetGenericTypeDefinition() == typeof (InlineCount<>)))
			//{
			//    result = (T) ExecuteInlineCount(queryModel);
			//}
			//else
			//{
				var data = ExecuteCollection<T>(queryModel);

				var result = data.SingleOrDefault();

				if ((returnDefaultWhenEmpty == false) && (data == null))
				{
					throw new InvalidOperationException("Sequence contains no elements.");
				}
			//}

			return result;
		}

		public IEnumerable<T> ExecuteCollection<T>(QueryModel queryModel)
		{
			return Execute<IEnumerable<T>>(queryModel);
		}
	}
}
