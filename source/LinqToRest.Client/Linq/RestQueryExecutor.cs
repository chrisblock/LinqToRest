using System;
using System.Collections.Generic;
using System.Linq;

using LinqToRest.Client.Http;

using Remotion.Linq;

namespace LinqToRest.Client.Linq
{
	public class RestQueryExecutor : IQueryExecutor
	{
		private readonly RestQueryModelVisitor _queryModelVisitor = new RestQueryModelVisitor();

		private readonly IHttpService _httpService;

		public RestQueryExecutor() : this(DependencyResolver.Current.GetInstance<IHttpService>())
		{
		}

		public RestQueryExecutor(IHttpService httpService)
		{
			_httpService = httpService;
		}

		private Uri GetUri(QueryModel queryModel)
		{
			var url = _queryModelVisitor.Translate(queryModel);

			var uri = new Uri(url);

			return uri;
		}

		private T Execute<T>(QueryModel queryModel)
		{
			var uri = GetUri(queryModel);

			var result = _httpService.Get<T>(uri);

			return result;
		}

		public T ExecuteScalar<T>(QueryModel queryModel)
		{
			return Execute<T>(queryModel);
		}

		public T ExecuteSingle<T>(QueryModel queryModel, bool returnDefaultWhenEmpty)
		{
			var data = ExecuteCollection<T>(queryModel);

			var result = data.SingleOrDefault();

			if ((returnDefaultWhenEmpty == false) && (data == null))
			{
				throw new InvalidOperationException("Sequence contains no elements.");
			}

			return result;
		}

		public IEnumerable<T> ExecuteCollection<T>(QueryModel queryModel)
		{
			return Execute<IEnumerable<T>>(queryModel);
		}
	}
}
