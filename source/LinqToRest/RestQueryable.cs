using System.Linq;
using System.Linq.Expressions;

using Remotion.Linq;
using Remotion.Linq.Parsing.Structure;

namespace LinqToRest
{
	public class RestQueryable<T> : QueryableBase<T>
	{
		public RestQueryable(IQueryParser queryParser, IQueryExecutor executor) : base(queryParser, executor)
		{
		}

		public RestQueryable(IQueryProvider provider) : base(provider)
		{
		}

		public RestQueryable(IQueryProvider provider, Expression expression) : base(provider, expression)
		{
		}
	}
}
