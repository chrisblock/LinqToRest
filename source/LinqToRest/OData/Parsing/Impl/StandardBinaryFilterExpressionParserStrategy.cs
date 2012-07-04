using System;
using System.Collections.Generic;

using LinqToRest.OData.Filters;
using LinqToRest.OData.Lexing;

namespace LinqToRest.OData.Parsing.Impl
{
	public class StandardBinaryFilterExpressionParserStrategy : IFilterExpressionParserStrategy
	{
		private readonly IFilterExpressionParserStrategy _filterExpressionParserStrategy;

		public StandardBinaryFilterExpressionParserStrategy(IFilterExpressionParserStrategy filterExpressionParserStrategy)
		{
			_filterExpressionParserStrategy = filterExpressionParserStrategy;
		}

		public FilterExpression BuildExpression(Stack<Token> stack)
		{
			if (stack == null)
			{
				throw new ArgumentNullException("stack", "Cannot build binary expression from null expression stack.");
			}

			if (stack.Count < 1)
			{
				throw new ArgumentException("Cannot build binary expression from empty expression stack.");
			}

			var token = stack.Pop();

			var op = token.Value;
			var binaryOperator = op.GetFromODataQueryOperatorString();

			if (binaryOperator.IsBinaryOperator() == false)
			{
				throw new ArgumentException(String.Format("Could not create binary expression with operator '{0}'. It is not a binary operator.", binaryOperator));
			}

			var right = _filterExpressionParserStrategy.BuildExpression(stack);

			if (right == null)
			{
				throw new ArgumentException("Could not create binary expression with null right side.");
			}

			var left = _filterExpressionParserStrategy.BuildExpression(stack);

			if (left == null)
			{
				throw new ArgumentException("Could not create binary expression with null left side.");
			}

			var result = FilterExpression.Binary(left, binaryOperator, right);

			return result;
		}
	}
}
