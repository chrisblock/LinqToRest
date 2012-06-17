using System;
using System.Collections.Generic;

using LinqToRest.OData.Filters;

namespace LinqToRest.OData.Building.Strategies.Impl
{
	public class FilterExpressionBuilderStrategy : IFilterExpressionBuilderStrategy
	{
		private readonly IDictionary<string, IFilterExpressionBuilderStrategy> _strategies = new Dictionary<string, IFilterExpressionBuilderStrategy>();

		private readonly IFilterExpressionBuilderStrategy _literalStrategy;

		public FilterExpressionBuilderStrategy()
		{
			_literalStrategy = new LiteralFilterExpressionBuilderStrategy();

			_strategies["->"] = new PropertyAccessFilterExpressionBuilderStrategy(this);

			_strategies[FilterExpressionOperator.Add.GetODataQueryOperatorString()] = new StandardBinaryFilterExpressionBuilderStrategy(this);
			_strategies[FilterExpressionOperator.And.GetODataQueryOperatorString()] = new StandardBinaryFilterExpressionBuilderStrategy(this);
			_strategies[FilterExpressionOperator.Divide.GetODataQueryOperatorString()] = new StandardBinaryFilterExpressionBuilderStrategy(this);
			_strategies[FilterExpressionOperator.Equal.GetODataQueryOperatorString()] = new StandardBinaryFilterExpressionBuilderStrategy(this);
			_strategies[FilterExpressionOperator.GreaterThan.GetODataQueryOperatorString()] = new StandardBinaryFilterExpressionBuilderStrategy(this);
			_strategies[FilterExpressionOperator.GreaterThanOrEqual.GetODataQueryOperatorString()] = new StandardBinaryFilterExpressionBuilderStrategy(this);
			_strategies[FilterExpressionOperator.LessThan.GetODataQueryOperatorString()] = new StandardBinaryFilterExpressionBuilderStrategy(this);
			_strategies[FilterExpressionOperator.LessThanOrEqual.GetODataQueryOperatorString()] = new StandardBinaryFilterExpressionBuilderStrategy(this);
			_strategies[FilterExpressionOperator.Modulo.GetODataQueryOperatorString()] = new StandardBinaryFilterExpressionBuilderStrategy(this);
			_strategies[FilterExpressionOperator.Multiply.GetODataQueryOperatorString()] = new StandardBinaryFilterExpressionBuilderStrategy(this);
			_strategies[FilterExpressionOperator.NotEqual.GetODataQueryOperatorString()] = new StandardBinaryFilterExpressionBuilderStrategy(this);
			_strategies[FilterExpressionOperator.Or.GetODataQueryOperatorString()] = new StandardBinaryFilterExpressionBuilderStrategy(this);
			_strategies[FilterExpressionOperator.Subtract.GetODataQueryOperatorString()] = new StandardBinaryFilterExpressionBuilderStrategy(this);

			_strategies[FilterExpressionOperator.Not.GetODataQueryOperatorString()] = new StandardUnaryFilterExpressionBuilderStrategy(this);
			_strategies[FilterExpressionOperator.Negate.GetODataQueryOperatorString()] = new StandardUnaryFilterExpressionBuilderStrategy(this);

			_strategies[Function.ToUpper.GetODataQueryMethodName()] = new FunctionFilterExpressionBuilderStrategy(this);
			_strategies[Function.ToLower.GetODataQueryMethodName()] = new FunctionFilterExpressionBuilderStrategy(this);
			_strategies[Function.Trim.GetODataQueryMethodName()] = new FunctionFilterExpressionBuilderStrategy(this);
			_strategies[Function.Length.GetODataQueryMethodName()] = new FunctionFilterExpressionBuilderStrategy(this);
			_strategies[Function.Year.GetODataQueryMethodName()] = new FunctionFilterExpressionBuilderStrategy(this);
			_strategies[Function.Month.GetODataQueryMethodName()] = new FunctionFilterExpressionBuilderStrategy(this);
			_strategies[Function.Day.GetODataQueryMethodName()] = new FunctionFilterExpressionBuilderStrategy(this);
			_strategies[Function.Hour.GetODataQueryMethodName()] = new FunctionFilterExpressionBuilderStrategy(this);
			_strategies[Function.Minute.GetODataQueryMethodName()] = new FunctionFilterExpressionBuilderStrategy(this);
			_strategies[Function.Second.GetODataQueryMethodName()] = new FunctionFilterExpressionBuilderStrategy(this);
			_strategies[Function.Years.GetODataQueryMethodName()] = new FunctionFilterExpressionBuilderStrategy(this);
			_strategies[Function.Days.GetODataQueryMethodName()] = new FunctionFilterExpressionBuilderStrategy(this);
			_strategies[Function.Hours.GetODataQueryMethodName()] = new FunctionFilterExpressionBuilderStrategy(this);
			_strategies[Function.Minutes.GetODataQueryMethodName()] = new FunctionFilterExpressionBuilderStrategy(this);
			_strategies[Function.Seconds.GetODataQueryMethodName()] = new FunctionFilterExpressionBuilderStrategy(this);
			_strategies[Function.Ceiling.GetODataQueryMethodName()] = new FunctionFilterExpressionBuilderStrategy(this);
			_strategies[Function.Floor.GetODataQueryMethodName()] = new FunctionFilterExpressionBuilderStrategy(this);
			_strategies[Function.Round.GetODataQueryMethodName()] = new FunctionFilterExpressionBuilderStrategy(this);

			_strategies[Function.Concat.GetODataQueryMethodName()] = new FunctionFilterExpressionBuilderStrategy(this);
			_strategies[Function.IsOf.GetODataQueryMethodName()] = new FunctionFilterExpressionBuilderStrategy(this);
			_strategies[Function.Cast.GetODataQueryMethodName()] = new FunctionFilterExpressionBuilderStrategy(this);
			_strategies[Function.IndexOf.GetODataQueryMethodName()] = new FunctionFilterExpressionBuilderStrategy(this);
			_strategies[Function.StartsWith.GetODataQueryMethodName()] = new FunctionFilterExpressionBuilderStrategy(this);
			_strategies[Function.EndsWith.GetODataQueryMethodName()] = new FunctionFilterExpressionBuilderStrategy(this);
			_strategies[Function.Substring.GetODataQueryMethodName()] = new FunctionFilterExpressionBuilderStrategy(this);
			_strategies[Function.SubstringOf.GetODataQueryMethodName()] = new FunctionFilterExpressionBuilderStrategy(this);

			_strategies[Function.Replace.GetODataQueryMethodName()] = new FunctionFilterExpressionBuilderStrategy(this);
		}

		public FilterExpression BuildExpression(Stack<string> stack)
		{
			if (stack == null)
			{
				throw new ArgumentNullException("stack", "Cannot build expression from null expression stack.");
			}

			if (stack.Count < 1)
			{
				throw new ArgumentException("Cannot build expression from empty expression stack", "stack");
			}

			var item = stack.Peek();

			IFilterExpressionBuilderStrategy strategy;
			var result = _strategies.TryGetValue(item, out strategy)
				? strategy.BuildExpression(stack)
				: _literalStrategy.BuildExpression(stack);

			return result;
		}
	}
}
