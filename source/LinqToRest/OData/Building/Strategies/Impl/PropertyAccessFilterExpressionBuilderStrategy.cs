using System.Collections.Generic;

using LinqToRest.OData.Filters;
using LinqToRest.OData.Literals;

namespace LinqToRest.OData.Building.Strategies.Impl
{
	public class PropertyAccessFilterExpressionBuilderStrategy : IFilterExpressionBuilderStrategy
	{
		private readonly IFilterExpressionBuilderStrategy _filterExpressionBuilderStrategy;

		public PropertyAccessFilterExpressionBuilderStrategy(IFilterExpressionBuilderStrategy filterExpressionBuilderStrategy)
		{
			_filterExpressionBuilderStrategy = filterExpressionBuilderStrategy;
		}

		public FilterExpression BuildExpression(Stack<Token> stack)
		{
			// pop off the member access operator ('.')
			stack.Pop();

			var token = stack.Pop();

			var memberName = token.Value;

			var memberParent = _filterExpressionBuilderStrategy.BuildExpression(stack);

			var result = FilterExpression.MemberAccess(memberParent, memberName);

			return result;
		}
	}
}
