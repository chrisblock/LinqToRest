using System.Linq;

using Remotion.Linq;
using Remotion.Linq.Parsing.Structure;

namespace LinqToRest
{
	public static class RestQueryableFactory
	{
		public static IQueryable<T> Create<T>(string url)
		{
			var parser = QueryParser.CreateDefault();
			IQueryExecutor executor = new RestQueryExecutor(url);

			var provider = new RestQueryProvider(parser, executor);

			return new RestQueryable<T>(provider);
		}
	}
}
