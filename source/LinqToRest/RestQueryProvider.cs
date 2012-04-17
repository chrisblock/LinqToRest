using System.Linq;
using System.Linq.Expressions;

using Remotion.Linq;
using Remotion.Linq.Parsing.Structure;

namespace LinqToRest
{
	public class RestQueryProvider : QueryProviderBase
	{
		public RestQueryProvider(IQueryParser queryParser, IQueryExecutor executor) : base(queryParser, executor)
		{
		}

		public override IQueryable<T> CreateQuery<T>(Expression expression)
		{
			return new RestQueryable<T>(this, expression);
		}
	}
}
