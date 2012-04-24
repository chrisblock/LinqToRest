using System;
using System.Collections.Generic;

namespace LinqToRest.OData.Strategies
{
	public class ArityBasedFunctionFilterExpressionBuilderStrategy : IODataFilterExpressionBuilderStrategy
	{
		private readonly IODataFilterExpressionBuilderStrategy _oDataFilterExpressionBuilderStrategy;
		private readonly int _arity;

		public ArityBasedFunctionFilterExpressionBuilderStrategy(IODataFilterExpressionBuilderStrategy oDataFilterExpressionBuilderStrategy, int arity)
		{
			_oDataFilterExpressionBuilderStrategy = oDataFilterExpressionBuilderStrategy;
			_arity = arity;
		}

		public string BuildExpression(Stack<string> stack)
		{
			var functionName = stack.Pop();
			var arguments = new List<string>();

			for (int i = 0; i < _arity; i++)
			{
				var argument = _oDataFilterExpressionBuilderStrategy.BuildExpression(stack);
				arguments.Add(argument);
			}

			arguments.Reverse();

			return String.Format("{0}({1})", functionName, String.Join(", ", arguments));
		}
	}
}
