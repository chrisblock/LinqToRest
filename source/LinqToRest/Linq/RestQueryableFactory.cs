using System.Linq;

using Remotion.Linq.Parsing.Structure;

namespace LinqToRest.Linq
{
	internal static class RestQueryableFactory
	{
		public static IQueryable<T> Create<T>()
		{
			var parser = QueryParser.CreateDefault();
			var executor = new RestQueryExecutor();

			var provider = new RestQueryProvider(parser, executor);

			return new RestQueryable<T>(provider);
		}
	}
}
