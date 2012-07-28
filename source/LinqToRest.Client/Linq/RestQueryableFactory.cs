using System.Linq;

using LinqToRest.Client.Http;

using Remotion.Linq.Parsing.Structure;

namespace LinqToRest.Client.Linq
{
	internal static class RestQueryableFactory
	{
		public static IQueryable<T> Create<T>()
		{
			var httpService = DependencyResolver.Current.GetInstance<IHttpService>();

			var parser = QueryParser.CreateDefault();
			var executor = new RestQueryExecutor(httpService);

			var provider = new RestQueryProvider(parser, executor);

			return new RestQueryable<T>(provider);
		}
	}
}
