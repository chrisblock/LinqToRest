using System.Collections.Generic;

using LinqToRest.OData.Filters;

namespace LinqToRest.OData.Building.Strategies.Impl
{
	public class ODataFilterExpressionBuilderStrategy : IODataFilterExpressionBuilderStrategy
	{
		private readonly IDictionary<string, IODataFilterExpressionBuilderStrategy> _strategies = new Dictionary<string, IODataFilterExpressionBuilderStrategy>();

		private readonly IODataFilterExpressionBuilderStrategy _literalStrategy;

		public ODataFilterExpressionBuilderStrategy()
		{
			_literalStrategy = new LiteralFilterExpressionBuilderStrategy();

			_strategies["->"] = new PropertyAccessFilterExpressionBuilderStrategy(this);

			_strategies[ODataQueryFilterExpressionOperator.Add.GetODataQueryOperatorString()] = new StandardBinaryFilterExpressionBuilderStrategy(this);
			_strategies[ODataQueryFilterExpressionOperator.And.GetODataQueryOperatorString()] = new StandardBinaryFilterExpressionBuilderStrategy(this);
			_strategies[ODataQueryFilterExpressionOperator.Divide.GetODataQueryOperatorString()] = new StandardBinaryFilterExpressionBuilderStrategy(this);
			_strategies[ODataQueryFilterExpressionOperator.Equal.GetODataQueryOperatorString()] = new StandardBinaryFilterExpressionBuilderStrategy(this);
			_strategies[ODataQueryFilterExpressionOperator.GreaterThan.GetODataQueryOperatorString()] = new StandardBinaryFilterExpressionBuilderStrategy(this);
			_strategies[ODataQueryFilterExpressionOperator.GreaterThanOrEqual.GetODataQueryOperatorString()] = new StandardBinaryFilterExpressionBuilderStrategy(this);
			_strategies[ODataQueryFilterExpressionOperator.LessThan.GetODataQueryOperatorString()] = new StandardBinaryFilterExpressionBuilderStrategy(this);
			_strategies[ODataQueryFilterExpressionOperator.LessThanOrEqual.GetODataQueryOperatorString()] = new StandardBinaryFilterExpressionBuilderStrategy(this);
			_strategies[ODataQueryFilterExpressionOperator.Modulo.GetODataQueryOperatorString()] = new StandardBinaryFilterExpressionBuilderStrategy(this);
			_strategies[ODataQueryFilterExpressionOperator.Multiply.GetODataQueryOperatorString()] = new StandardBinaryFilterExpressionBuilderStrategy(this);
			_strategies[ODataQueryFilterExpressionOperator.NotEqual.GetODataQueryOperatorString()] = new StandardBinaryFilterExpressionBuilderStrategy(this);
			_strategies[ODataQueryFilterExpressionOperator.Or.GetODataQueryOperatorString()] = new StandardBinaryFilterExpressionBuilderStrategy(this);
			_strategies[ODataQueryFilterExpressionOperator.Subtract.GetODataQueryOperatorString()] = new StandardBinaryFilterExpressionBuilderStrategy(this);

			_strategies[ODataQueryFilterExpressionOperator.Not.GetODataQueryOperatorString()] = new StandardUnaryFilterExpressionBuilderStrategy(this);
			_strategies[ODataQueryFilterExpressionOperator.Negate.GetODataQueryOperatorString()] = new StandardUnaryFilterExpressionBuilderStrategy(this);

			_strategies[Function.ToUpper.GetODataQueryMethodName()] = new ArityBasedFunctionFilterExpressionBuilderStrategy(this, 1);
			_strategies[Function.ToLower.GetODataQueryMethodName()] = new ArityBasedFunctionFilterExpressionBuilderStrategy(this, 1);
			_strategies[Function.Trim.GetODataQueryMethodName()] = new ArityBasedFunctionFilterExpressionBuilderStrategy(this, 1);
			_strategies[Function.Length.GetODataQueryMethodName()] = new ArityBasedFunctionFilterExpressionBuilderStrategy(this, 1);
			_strategies[Function.Year.GetODataQueryMethodName()] = new ArityBasedFunctionFilterExpressionBuilderStrategy(this, 1);
			_strategies[Function.Month.GetODataQueryMethodName()] = new ArityBasedFunctionFilterExpressionBuilderStrategy(this, 1);
			_strategies[Function.Day.GetODataQueryMethodName()] = new ArityBasedFunctionFilterExpressionBuilderStrategy(this, 1);
			_strategies[Function.Hour.GetODataQueryMethodName()] = new ArityBasedFunctionFilterExpressionBuilderStrategy(this, 1);
			_strategies[Function.Minute.GetODataQueryMethodName()] = new ArityBasedFunctionFilterExpressionBuilderStrategy(this, 1);
			_strategies[Function.Second.GetODataQueryMethodName()] = new ArityBasedFunctionFilterExpressionBuilderStrategy(this, 1);
			_strategies[Function.Years.GetODataQueryMethodName()] = new ArityBasedFunctionFilterExpressionBuilderStrategy(this, 1);
			_strategies[Function.Days.GetODataQueryMethodName()] = new ArityBasedFunctionFilterExpressionBuilderStrategy(this, 1);
			_strategies[Function.Hours.GetODataQueryMethodName()] = new ArityBasedFunctionFilterExpressionBuilderStrategy(this, 1);
			_strategies[Function.Minutes.GetODataQueryMethodName()] = new ArityBasedFunctionFilterExpressionBuilderStrategy(this, 1);
			_strategies[Function.Seconds.GetODataQueryMethodName()] = new ArityBasedFunctionFilterExpressionBuilderStrategy(this, 1);
			_strategies[Function.Ceiling.GetODataQueryMethodName()] = new ArityBasedFunctionFilterExpressionBuilderStrategy(this, 1);
			_strategies[Function.Floor.GetODataQueryMethodName()] = new ArityBasedFunctionFilterExpressionBuilderStrategy(this, 1);
			_strategies[Function.Round.GetODataQueryMethodName()] = new ArityBasedFunctionFilterExpressionBuilderStrategy(this, 1);

			_strategies[Function.Concat.GetODataQueryMethodName()] = new ArityBasedFunctionFilterExpressionBuilderStrategy(this, 2);
			_strategies[Function.IsOf.GetODataQueryMethodName()] = new ArityBasedFunctionFilterExpressionBuilderStrategy(this, 2);
			_strategies[Function.Cast.GetODataQueryMethodName()] = new ArityBasedFunctionFilterExpressionBuilderStrategy(this, 2);
			_strategies[Function.IndexOf.GetODataQueryMethodName()] = new ArityBasedFunctionFilterExpressionBuilderStrategy(this, 2);
			_strategies[Function.StartsWith.GetODataQueryMethodName()] = new ArityBasedFunctionFilterExpressionBuilderStrategy(this, 2);
			_strategies[Function.EndsWith.GetODataQueryMethodName()] = new ArityBasedFunctionFilterExpressionBuilderStrategy(this, 2);
			_strategies[Function.Substring.GetODataQueryMethodName()] = new ArityBasedFunctionFilterExpressionBuilderStrategy(this, 2);
			_strategies[Function.SubstringOf.GetODataQueryMethodName()] = new ArityBasedFunctionFilterExpressionBuilderStrategy(this, 2);

			_strategies[Function.Replace.GetODataQueryMethodName()] = new ArityBasedFunctionFilterExpressionBuilderStrategy(this, 3);
		}

		public ODataQueryFilterExpression BuildExpression(Stack<string> stack)
		{
			var item = stack.Peek();

			IODataFilterExpressionBuilderStrategy strategy;
			var result = _strategies.TryGetValue(item, out strategy)
				? strategy.BuildExpression(stack)
				: _literalStrategy.BuildExpression(stack);

			return result;
		}
	}
}
