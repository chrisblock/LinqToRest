using System;
using System.Collections.Generic;

namespace LinqToRest.OData.Strategies
{
	public class PropertyAccessFilterExpressionBuilderStrategy : IODataFilterExpressionBuilderStrategy
	{
		private readonly IODataFilterExpressionBuilderStrategy _oDataFilterExpressionBuilderStrategy;

		public PropertyAccessFilterExpressionBuilderStrategy(IODataFilterExpressionBuilderStrategy oDataFilterExpressionBuilderStrategy)
		{
			_oDataFilterExpressionBuilderStrategy = oDataFilterExpressionBuilderStrategy;
		}

		public string BuildExpression(Stack<string> stack)
		{
			// pop off the member access operator ('->')
			stack.Pop();

			var memberName = stack.Pop();

			var memberParent = _oDataFilterExpressionBuilderStrategy.BuildExpression(stack);

			return String.Format("{0}/{1}", memberParent, memberName);
		}
	}
}
