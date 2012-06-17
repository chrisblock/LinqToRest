using System.Collections.Generic;

using LinqToRest.OData.Filters;

namespace LinqToRest.OData.Building.Strategies.Impl
{
	public class PropertyAccessFilterExpressionBuilderStrategy : IFilterExpressionBuilderStrategy
	{
		private readonly IFilterExpressionBuilderStrategy _filterExpressionBuilderStrategy;

		public PropertyAccessFilterExpressionBuilderStrategy(IFilterExpressionBuilderStrategy filterExpressionBuilderStrategy)
		{
			_filterExpressionBuilderStrategy = filterExpressionBuilderStrategy;
		}

		public FilterExpression BuildExpression(Stack<string> stack)
		{
			// pop off the member access operator ('->')
			stack.Pop();

			var memberName = stack.Pop();

			var memberParent = _filterExpressionBuilderStrategy.BuildExpression(stack);

			var result = FilterExpression.MemberAccess(memberParent, memberName);

			return result;
		}
	}
}
