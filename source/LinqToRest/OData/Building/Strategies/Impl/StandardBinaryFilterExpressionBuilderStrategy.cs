using System.Collections.Generic;

using LinqToRest.OData.Filters;

namespace LinqToRest.OData.Building.Strategies.Impl
{
	public class StandardBinaryFilterExpressionBuilderStrategy : IODataFilterExpressionBuilderStrategy
	{
		private readonly IODataFilterExpressionBuilderStrategy _oDataFilterExpressionBuilderStrategy;

		public StandardBinaryFilterExpressionBuilderStrategy(IODataFilterExpressionBuilderStrategy oDataFilterExpressionBuilderStrategy)
		{
			_oDataFilterExpressionBuilderStrategy = oDataFilterExpressionBuilderStrategy;
		}

		public ODataQueryFilterExpression BuildExpression(Stack<string> stack)
		{
			var op = stack.Pop();
			var binaryOperator = op.GetFromODataQueryOperatorString();
			var right = _oDataFilterExpressionBuilderStrategy.BuildExpression(stack);
			var left = _oDataFilterExpressionBuilderStrategy.BuildExpression(stack);

			var result = ODataQueryFilterExpression.Binary(left, binaryOperator, right);

			return result;
		}
	}
}
