using System.Collections.Generic;

using LinqToRest.OData.Filters;
using LinqToRest.OData.Lexing;

namespace LinqToRest.OData.Parsing.Impl
{
	public class PropertyAccessFilterExpressionParserStrategy : IFilterExpressionParserStrategy
	{
		private readonly IFilterExpressionParserStrategy _filterExpressionParserStrategy;

		public PropertyAccessFilterExpressionParserStrategy(IFilterExpressionParserStrategy filterExpressionParserStrategy)
		{
			_filterExpressionParserStrategy = filterExpressionParserStrategy;
		}

		public FilterExpression BuildExpression(Stack<Token> stack)
		{
			// pop off the member access operator ('.')
			stack.Pop();

			var token = stack.Pop();

			var memberName = token.Value;

			var memberParent = _filterExpressionParserStrategy.BuildExpression(stack);

			var result = FilterExpression.MemberAccess(memberParent, memberName);

			return result;
		}
	}
}
