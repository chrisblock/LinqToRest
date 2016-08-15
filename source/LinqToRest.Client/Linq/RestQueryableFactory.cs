using System;
using System.Linq;

using LinqToRest.Client.Http;

using Remotion.Linq.Parsing.Structure;

namespace LinqToRest.Client.Linq
{
	public class RestQueryableFactory : IRestQueryableFactory
	{
		private readonly IHttpService _httpService;
		private readonly IQueryModelTranslator _queryModelTranslator;

		public RestQueryableFactory(IHttpService httpService, IQueryModelTranslator queryModelTranslator)
		{
			_httpService = httpService;
			_queryModelTranslator = queryModelTranslator;
		}

		public IQueryable<T> Create<T>(string url)
		{
			return Create<T>(new Uri(url));
		}

		public IQueryable<T> Create<T>(Uri uri)
		{
			var parser = QueryParser.CreateDefault();

			var executor = new RestQueryExecutor(uri, _queryModelTranslator, _httpService);

			var provider = new RestQueryProvider(parser, executor);

			return new RestQueryable<T>(provider);
		}
	}
}
