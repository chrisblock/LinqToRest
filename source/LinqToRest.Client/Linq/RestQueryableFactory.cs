using System.Linq;

using LinqToRest.Client.Http;
using LinqToRest.OData;

using Remotion.Linq.Parsing.Structure;

namespace LinqToRest.Client.Linq
{
	internal static class RestQueryableFactory
	{
		public static IQueryable<T> Create<T>()
		{
			var httpService = DependencyResolver.Current.GetInstance<IHttpService>();
			var queryFactory = DependencyResolver.Current.GetInstance<IODataQueryFactory>();
			var queryModelTranslator = new RestQueryModelVisitor(queryFactory);

			var parser = QueryParser.CreateDefault();
			var executor = new RestQueryExecutor(queryModelTranslator, httpService);

			var provider = new RestQueryProvider(parser, executor);

			return new RestQueryable<T>(provider);
		}
	}
}
