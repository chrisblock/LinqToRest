using System;
using System.Collections.Generic;

using LinqToRest.OData.Filters;
using LinqToRest.OData.Literals;

namespace LinqToRest.OData.Building.Strategies.Impl
{
	public class StandardUnaryFilterExpressionBuilderStrategy : IFilterExpressionBuilderStrategy
	{
		private readonly IFilterExpressionBuilderStrategy _filterExpressionBuilderStrategy;

		public StandardUnaryFilterExpressionBuilderStrategy(IFilterExpressionBuilderStrategy filterExpressionBuilderStrategy)
		{
			_filterExpressionBuilderStrategy = filterExpressionBuilderStrategy;
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

			var op = token.Value;
			var unaryOperator = op.GetFromODataQueryOperatorString();

			if (unaryOperator.IsUnaryOperator() == false)
			{
				throw new ArgumentException(String.Format("Cannot build unary expression with operator '{0}'. It is not a unary operator.", unaryOperator));
			}

			var operand = _filterExpressionBuilderStrategy.BuildExpression(stack);

			if (operand == null)
			{
				throw new ApplicationException("Cannot use a null operand when building a unary expression.");
			}

			var result = FilterExpression.Unary(unaryOperator, operand);

			return result;
		}
	}
}
