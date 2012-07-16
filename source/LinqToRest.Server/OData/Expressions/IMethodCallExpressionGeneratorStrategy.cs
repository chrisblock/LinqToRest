using System.Collections.Generic;
using System.Linq.Expressions;

using LinqToRest.OData.Filters;

namespace LinqToRest.Server.OData.Expressions
{
	public interface IMethodCallExpressionGeneratorStrategy
	{
		Expression Generate(Function method, IEnumerable<Expression> arguments);
	}
}
