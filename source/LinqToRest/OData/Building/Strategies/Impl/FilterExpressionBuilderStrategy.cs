using System;
using System.Collections.Generic;

using LinqToRest.OData.Filters;
using LinqToRest.OData.Literals;

namespace LinqToRest.OData.Building.Strategies.Impl
{
	public class FilterExpressionBuilderStrategy : IFilterExpressionBuilderStrategy
	{
		private readonly IDictionary<TokenType, IFilterExpressionBuilderStrategy> _strategies = new Dictionary<TokenType, IFilterExpressionBuilderStrategy>();

		private readonly IFilterExpressionBuilderStrategy _literalStrategy;

		public FilterExpressionBuilderStrategy()
		{
			_literalStrategy = new LiteralFilterExpressionBuilderStrategy();

			_strategies[TokenType.MemberAccess] = new PropertyAccessFilterExpressionBuilderStrategy(this);

			_strategies[TokenType.BinaryOperator] = new StandardBinaryFilterExpressionBuilderStrategy(this);

			_strategies[TokenType.UnaryOperator] = new StandardUnaryFilterExpressionBuilderStrategy(this);

			_strategies[TokenType.Function] = new FunctionFilterExpressionBuilderStrategy(this);
		}

		public FilterExpression BuildExpression(Stack<Token> stack)
		{
			if (stack == null)
			{
				throw new ArgumentNullException("stack", "Cannot build expression from null expression stack.");
			}

			if (stack.Count < 1)
			{
				throw new ArgumentException("Cannot build expression from empty expression stack.", "stack");
			}

			var item = stack.Peek();

			IFilterExpressionBuilderStrategy strategy;

			if (_strategies.TryGetValue(item.TokenType, out strategy) == false)
			{
				strategy = _literalStrategy;
			}

			var result = strategy.BuildExpression(stack);

			return result;
		}
	}
}
