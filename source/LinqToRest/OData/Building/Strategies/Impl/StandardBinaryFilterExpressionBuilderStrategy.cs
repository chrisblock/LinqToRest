using System;
using System.Collections.Generic;

using LinqToRest.OData.Filters;

namespace LinqToRest.OData.Building.Strategies.Impl
{
	public class StandardBinaryFilterExpressionBuilderStrategy : IFilterExpressionBuilderStrategy
	{
		private readonly IFilterExpressionBuilderStrategy _filterExpressionBuilderStrategy;

		public StandardBinaryFilterExpressionBuilderStrategy(IFilterExpressionBuilderStrategy filterExpressionBuilderStrategy)
		{
			_filterExpressionBuilderStrategy = filterExpressionBuilderStrategy;
		}

		public FilterExpression BuildExpression(Stack<string> stack)
		{
			if (stack == null)
			{
				throw new ArgumentNullException("stack", "Cannot build binary expression from null expression stack.");
			}

			if (stack.Count < 1)
			{
				throw new ArgumentException("Cannot build binary expression from empty expression stack.");
			}

			var op = stack.Pop();
			var binaryOperator = op.GetFromODataQueryOperatorString();

			if (binaryOperator.IsBinaryOperator() == false)
			{
				throw new ArgumentException(String.Format("Could not create binary expression with operator '{0}'. It is not a binary operator.", binaryOperator));
			}

			var right = _filterExpressionBuilderStrategy.BuildExpression(stack);

			if (right == null)
			{
				throw new ArgumentException("Could not create binary expression with null right side.");
			}

			var left = _filterExpressionBuilderStrategy.BuildExpression(stack);

			if (left == null)
			{
				throw new ArgumentException("Could not create binary expression with null left side.");
			}

			var result = FilterExpression.Binary(left, binaryOperator, right);

			return result;
		}
	}
}
