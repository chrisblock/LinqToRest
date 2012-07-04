using System;
using System.Collections.Generic;

using LinqToRest.OData.Filters;
using LinqToRest.OData.Lexing;

namespace LinqToRest.OData.Parsing.Impl
{
	public class FilterExpressionParserStrategy : IFilterExpressionParserStrategy
	{
		private readonly IDictionary<TokenType, IFilterExpressionParserStrategy> _strategies = new Dictionary<TokenType, IFilterExpressionParserStrategy>();

		public FilterExpressionParserStrategy()
		{
			_strategies[TokenType.Boolean] = new BooleanFilterExpressionParserStrategy();
			_strategies[TokenType.Byte] = new ByteFilterExpressionParserStrategy();
			_strategies[TokenType.DateTime] = new DateTimeFilterExpressionParserStrategy();
			_strategies[TokenType.DateTimeOffset] = new DateTimeOffsetFilterExpressionParserStrategy();
			_strategies[TokenType.Decimal] = new DecimalFilterExpressionParserStrategy();
			_strategies[TokenType.Double] = new DoubleFilterExpressionParserStrategy();
			_strategies[TokenType.Float] = new FloatFilterExpressionParserStrategy();
			_strategies[TokenType.Guid] = new GuidFilterExpressionParserStrategy();
			_strategies[TokenType.Integer] = new IntegerFilterExpressionParserStrategy();
			_strategies[TokenType.Long] = new LongFilterExpressionParserStrategy();
			_strategies[TokenType.Name] = new NameFilterExpressionParserStrategy();
			_strategies[TokenType.Null] = new NullFilterExpressionParserStrategy();
			_strategies[TokenType.Short] = new ShortFilterExpressionParserStrategy();
			_strategies[TokenType.String] = new StringFilterExpressionParserStrategy();
			_strategies[TokenType.Time] = new TimeFilterExpressionParserStrategy();
			_strategies[TokenType.Primitive] = new PrimitiveFilterExpressionParserStrategy();

			_strategies[TokenType.MemberAccess] = new PropertyAccessFilterExpressionParserStrategy(this);

			_strategies[TokenType.BinaryOperator] = new StandardBinaryFilterExpressionParserStrategy(this);

			_strategies[TokenType.UnaryOperator] = new StandardUnaryFilterExpressionParserStrategy(this);

			_strategies[TokenType.Function] = new FunctionFilterExpressionParserStrategy(this);
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

			var token = stack.Peek();

			IFilterExpressionParserStrategy strategy;

			if (_strategies.TryGetValue(token.TokenType, out strategy) == false)
			{
				throw new ArgumentException(String.Format("Could not find IFilterExpressionParserStrategy for TokenType '{0}'.", token.TokenType));
			}

			var result = strategy.BuildExpression(stack);

			return result;
		}
	}
}
