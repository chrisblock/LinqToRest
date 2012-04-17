using System;
using System.Collections.Generic;

namespace LinqToRest.OpenData.Strategies
{
	public class StandardBinaryFilterExpressionBuilderStrategy : IODataFilterExpressionBuilderStrategy
	{
		private readonly IODataFilterExpressionBuilderStrategy _oDataFilterExpressionBuilderStrategy;

		public StandardBinaryFilterExpressionBuilderStrategy(IODataFilterExpressionBuilderStrategy oDataFilterExpressionBuilderStrategy)
		{
			_oDataFilterExpressionBuilderStrategy = oDataFilterExpressionBuilderStrategy;
		}

		public string BuildExpression(Stack<string> stack)
		{
			var binaryOperator = stack.Pop();
			var right = _oDataFilterExpressionBuilderStrategy.BuildExpression(stack);
			var left = _oDataFilterExpressionBuilderStrategy.BuildExpression(stack);

			return String.Format("({0} {1} {2})", left, binaryOperator, right);
		}
	}
}
