using System.Collections.Generic;

using LinqToRest.OData.Filters;

namespace LinqToRest.OData.Building.Strategies.Impl
{
	public class StandardUnaryFilterExpressionBuilderStrategy : IODataFilterExpressionBuilderStrategy
	{
		private readonly IODataFilterExpressionBuilderStrategy _oDataFilterExpressionBuilderStrategy;

		public StandardUnaryFilterExpressionBuilderStrategy(IODataFilterExpressionBuilderStrategy oDataFilterExpressionBuilderStrategy)
		{
			_oDataFilterExpressionBuilderStrategy = oDataFilterExpressionBuilderStrategy;
		}

		public ODataQueryFilterExpression BuildExpression(Stack<string> stack)
		{
			var op = stack.Pop();
			var unaryOperator = op.GetFromODataQueryOperatorString();
			var operand = _oDataFilterExpressionBuilderStrategy.BuildExpression(stack);

			var result = ODataQueryFilterExpression.Unary(unaryOperator, operand);

			return result;
		}
	}
}
