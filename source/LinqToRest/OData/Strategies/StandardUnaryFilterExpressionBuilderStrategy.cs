using System;
using System.Collections.Generic;

namespace LinqToRest.OData.Strategies
{
	public class StandardUnaryFilterExpressionBuilderStrategy : IODataFilterExpressionBuilderStrategy
	{
		private readonly IODataFilterExpressionBuilderStrategy _oDataFilterExpressionBuilderStrategy;

		public StandardUnaryFilterExpressionBuilderStrategy(IODataFilterExpressionBuilderStrategy oDataFilterExpressionBuilderStrategy)
		{
			_oDataFilterExpressionBuilderStrategy = oDataFilterExpressionBuilderStrategy;
		}

		public string BuildExpression(Stack<string> stack)
		{
			var unaryOperator = stack.Pop();
			var operand = _oDataFilterExpressionBuilderStrategy.BuildExpression(stack);

			return String.Format("({0}({1}))", unaryOperator, operand);
		}
	}
}
