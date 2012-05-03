using System.Collections.Generic;

using LinqToRest.OData.Filters;

namespace LinqToRest.OData.Building.Strategies
{
	public interface IODataFilterExpressionBuilderStrategy
	{
		ODataQueryFilterExpression BuildExpression(Stack<string> stack);
	}
}
