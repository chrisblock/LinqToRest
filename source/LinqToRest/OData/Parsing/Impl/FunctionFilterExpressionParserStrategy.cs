using System;
using System.Collections.Generic;

using LinqToRest.OData.Filters;
using LinqToRest.OData.Lexing;

namespace LinqToRest.OData.Parsing.Impl
{
	public class FunctionFilterExpressionParserStrategy : IFilterExpressionParserStrategy
	{
		private readonly IFilterExpressionParserStrategy _filterExpressionParserStrategy;

		public FunctionFilterExpressionParserStrategy(IFilterExpressionParserStrategy filterExpressionParserStrategy)
		{
			_filterExpressionParserStrategy = filterExpressionParserStrategy;
		}

		public FilterExpression BuildExpression(Stack<Token> stack)
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
			var function = fn.Value.GetFromODataQueryMethodName();

			var arity = function.Arity();

			if (stack.Count < arity)
			{
				throw new ArgumentException(String.Format("Cannot build function expression for function '{0}'. Not enough parameters.", function));
			}

			var arguments = new List<FilterExpression>();

			for (int i = 0; i < arity; i++)
			{
				var argument = _filterExpressionParserStrategy.BuildExpression(stack);
				arguments.Add(argument);
			}

			arguments.Reverse();

			var result = FilterExpression.MethodCall(function, arguments.ToArray());

			return result;
		}
	}
}
