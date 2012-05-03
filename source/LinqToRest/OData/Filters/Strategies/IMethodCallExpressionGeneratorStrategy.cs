using System.Collections.Generic;
using System.Linq.Expressions;

namespace LinqToRest.OData.Filters.Strategies
{
	public interface IMethodCallExpressionGeneratorStrategy
	{
		Expression Generate(Function method, IEnumerable<Expression> arguments);
	}
}
