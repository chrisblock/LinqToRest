using System;
using System.Collections.Generic;

namespace LinqToRest.OpenData.Strategies
{
	public class TwoParameterFunctionBinaryFilterExpressionBuilderStrategy : IODataFilterExpressionBuilderStrategy
	{
		private readonly IODataFilterExpressionBuilderStrategy _oDataFilterExpressionBuilderStrategy;

		public TwoParameterFunctionBinaryFilterExpressionBuilderStrategy(IODataFilterExpressionBuilderStrategy oDataFilterExpressionBuilderStrategy)
		{
			_oDataFilterExpressionBuilderStrategy = oDataFilterExpressionBuilderStrategy;
		}

		public string BuildExpression(Stack<string> stack)
		{
			var binaryOperator = stack.Pop();
			var left = _oDataFilterExpressionBuilderStrategy.BuildExpression(stack);
			var right = _oDataFilterExpressionBuilderStrategy.BuildExpression(stack);

			return String.Format("{0}({1}, {2})", binaryOperator, left, right);
		}
	}
}
