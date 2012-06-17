using System.Collections.Generic;

using LinqToRest.OData.Filters;

namespace LinqToRest.OData.Building.Strategies
{
	public interface IFilterExpressionBuilderStrategy
	{
		FilterExpression BuildExpression(Stack<string> stack);
	}
}
