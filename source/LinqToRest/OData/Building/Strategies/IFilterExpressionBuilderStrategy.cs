using System.Collections.Generic;

using LinqToRest.OData.Filters;
using LinqToRest.OData.Literals;

namespace LinqToRest.OData.Building.Strategies
{
	public interface IFilterExpressionBuilderStrategy
	{
		FilterExpression BuildExpression(Stack<Token> stack);
	}
}
