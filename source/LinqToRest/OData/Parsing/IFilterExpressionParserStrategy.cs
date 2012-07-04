using System.Collections.Generic;

using LinqToRest.OData.Filters;
using LinqToRest.OData.Lexing;

namespace LinqToRest.OData.Parsing
{
	public interface IFilterExpressionParserStrategy
	{
		FilterExpression BuildExpression(Stack<Token> stack);
	}
}
