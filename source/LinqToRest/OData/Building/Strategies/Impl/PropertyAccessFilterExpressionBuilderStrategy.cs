using System.Collections.Generic;

using LinqToRest.OData.Filters;

namespace LinqToRest.OData.Building.Strategies.Impl
{
	public class PropertyAccessFilterExpressionBuilderStrategy : IODataFilterExpressionBuilderStrategy
	{
		private readonly IODataFilterExpressionBuilderStrategy _oDataFilterExpressionBuilderStrategy;

		public PropertyAccessFilterExpressionBuilderStrategy(IODataFilterExpressionBuilderStrategy oDataFilterExpressionBuilderStrategy)
		{
			_oDataFilterExpressionBuilderStrategy = oDataFilterExpressionBuilderStrategy;
		}

		public ODataQueryFilterExpression BuildExpression(Stack<string> stack)
		{
			// pop off the member access operator ('->')
			stack.Pop();

			var memberName = stack.Pop();

			var memberParent = _oDataFilterExpressionBuilderStrategy.BuildExpression(stack);

			var result = ODataQueryFilterExpression.MemberAccess(memberParent, memberName);

			return result;
		}
	}
}
