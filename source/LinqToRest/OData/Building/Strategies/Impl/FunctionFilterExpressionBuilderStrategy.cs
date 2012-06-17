using System;
using System.Collections.Generic;

using LinqToRest.OData.Filters;

namespace LinqToRest.OData.Building.Strategies.Impl
{
	public class FunctionFilterExpressionBuilderStrategy : IFilterExpressionBuilderStrategy
	{
		private readonly IFilterExpressionBuilderStrategy _filterExpressionBuilderStrategy;

		public FunctionFilterExpressionBuilderStrategy(IFilterExpressionBuilderStrategy filterExpressionBuilderStrategy)
		{
			_filterExpressionBuilderStrategy = filterExpressionBuilderStrategy;
		}

		public FilterExpression BuildExpression(Stack<string> stack)
		{
			if (stack == null)
			{
				throw new ArgumentNullException("stack", "Cannot build function expression with a null expression stack.");
			}

			if (stack.Count < 1)
			{
				throw new ArgumentException("Cannot build function expression for function '{0}'. Not enough parameters.");
			}

			var fn = stack.Pop();
			var function = fn.GetFromODataQueryMethodName();

			var arity = function.Arity();

			if (stack.Count < arity)
			{
				throw new ArgumentException(String.Format("Cannot build function expression for function '{0}'. Not enough parameters.", function));
			}

			var arguments = new List<FilterExpression>();

			for (int i = 0; i < arity; i++)
			{
				var argument = _filterExpressionBuilderStrategy.BuildExpression(stack);
				arguments.Add(argument);
			}

			arguments.Reverse();

			var result = FilterExpression.MethodCall(function, arguments.ToArray());

			return result;
		}
	}
}
