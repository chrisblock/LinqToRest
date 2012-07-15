using System;
using System.Collections.Generic;

using LinqToRest.OData.Filters;
using LinqToRest.OData.Lexing;

namespace LinqToRest.OData.Parsing.Impl
{
	public class StandardUnaryFilterExpressionParserStrategy : IFilterExpressionParserStrategy
	{
		private readonly IFilterExpressionParserStrategy _filterExpressionParserStrategy;

		public StandardUnaryFilterExpressionParserStrategy(IFilterExpressionParserStrategy filterExpressionParserStrategy)
		{
			_filterExpressionParserStrategy = filterExpressionParserStrategy;
		}

		public FilterExpression BuildExpression(Stack<Token> stack)
		{
			if (stack == null)
			{
				throw new ArgumentNullException("stack", "Cannot build unary expression from null expression stack.");
			}

			if (stack.Count < 1)
			{
				throw new ArgumentException("Cannot build unary expression from empty expression stack.");
			}

			var token = stack.Pop();

			if (token == null)
			{
				throw new ArgumentException("Cannot parse a null token.");
			}

			var op = token.Value;
			var unaryOperator = op.GetFromODataQueryOperatorString();

			if (unaryOperator.IsUnaryOperator() == false)
			{
				throw new ArgumentException(String.Format("Cannot build unary expression with operator '{0}'. It is not a unary operator.", unaryOperator));
			}

			var operand = _filterExpressionParserStrategy.BuildExpression(stack);

			if (operand == null)
			{
				throw new ApplicationException("Cannot use a null operand when building a unary expression.");
			}

			var result = FilterExpression.Unary(unaryOperator, operand);

			return result;
		}
	}
}
