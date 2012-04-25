using System.Collections.Generic;

namespace LinqToRest.OData.Strategies
{
	public class ODataFilterExpressionBuilderStrategy: IODataFilterExpressionBuilderStrategy
	{
		private readonly IDictionary<string, IODataFilterExpressionBuilderStrategy> _strategies = new Dictionary<string, IODataFilterExpressionBuilderStrategy>();

		public ODataFilterExpressionBuilderStrategy()
		{
			_strategies["->"] = new PropertyAccessFilterExpressionBuilderStrategy(this);

			_strategies[FilterOperators.Add] = new StandardBinaryFilterExpressionBuilderStrategy(this);
			_strategies[FilterOperators.And] = new StandardBinaryFilterExpressionBuilderStrategy(this);
			_strategies[FilterOperators.Divide] = new StandardBinaryFilterExpressionBuilderStrategy(this);
			_strategies[FilterOperators.Equal] = new StandardBinaryFilterExpressionBuilderStrategy(this);
			_strategies[FilterOperators.GreaterThan] = new StandardBinaryFilterExpressionBuilderStrategy(this);
			_strategies[FilterOperators.GreaterThanOrEqual] = new StandardBinaryFilterExpressionBuilderStrategy(this);
			_strategies[FilterOperators.LessThan] = new StandardBinaryFilterExpressionBuilderStrategy(this);
			_strategies[FilterOperators.LessThanOrEqual] = new StandardBinaryFilterExpressionBuilderStrategy(this);
			_strategies[FilterOperators.Modulo] = new StandardBinaryFilterExpressionBuilderStrategy(this);
			_strategies[FilterOperators.Multiply] = new StandardBinaryFilterExpressionBuilderStrategy(this);
			_strategies[FilterOperators.NotEqual] = new StandardBinaryFilterExpressionBuilderStrategy(this);
			_strategies[FilterOperators.Or] = new StandardBinaryFilterExpressionBuilderStrategy(this);
			_strategies[FilterOperators.Subtract] = new StandardBinaryFilterExpressionBuilderStrategy(this);

			_strategies[FilterOperators.Not] = new StandardUnaryFilterExpressionBuilderStrategy(this);
			_strategies[FilterOperators.Negate] = new StandardUnaryFilterExpressionBuilderStrategy(this);

			_strategies[FilterFunctions.ToUpper] = new ArityBasedFunctionFilterExpressionBuilderStrategy(this, 1);
			_strategies[FilterFunctions.ToLower] = new ArityBasedFunctionFilterExpressionBuilderStrategy(this, 1);
			_strategies[FilterFunctions.Trim] = new ArityBasedFunctionFilterExpressionBuilderStrategy(this, 1);
			_strategies[FilterFunctions.Length] = new ArityBasedFunctionFilterExpressionBuilderStrategy(this, 1);
			_strategies[FilterFunctions.Year] = new ArityBasedFunctionFilterExpressionBuilderStrategy(this, 1);
			_strategies[FilterFunctions.Month] = new ArityBasedFunctionFilterExpressionBuilderStrategy(this, 1);
			_strategies[FilterFunctions.Day] = new ArityBasedFunctionFilterExpressionBuilderStrategy(this, 1);
			_strategies[FilterFunctions.Hour] = new ArityBasedFunctionFilterExpressionBuilderStrategy(this, 1);
			_strategies[FilterFunctions.Minute] = new ArityBasedFunctionFilterExpressionBuilderStrategy(this, 1);
			_strategies[FilterFunctions.Second] = new ArityBasedFunctionFilterExpressionBuilderStrategy(this, 1);
			_strategies[FilterFunctions.Ceiling] = new ArityBasedFunctionFilterExpressionBuilderStrategy(this, 1);
			_strategies[FilterFunctions.Floor] = new ArityBasedFunctionFilterExpressionBuilderStrategy(this, 1);
			_strategies[FilterFunctions.Round] = new ArityBasedFunctionFilterExpressionBuilderStrategy(this, 1);

			_strategies[FilterFunctions.IsOf] = new ArityBasedFunctionFilterExpressionBuilderStrategy(this, 2);
			_strategies[FilterFunctions.Cast] = new ArityBasedFunctionFilterExpressionBuilderStrategy(this, 2);
			_strategies[FilterFunctions.IndexOf] = new ArityBasedFunctionFilterExpressionBuilderStrategy(this, 2);
			_strategies[FilterFunctions.StartsWith] = new ArityBasedFunctionFilterExpressionBuilderStrategy(this, 2);
			_strategies[FilterFunctions.EndsWith] = new ArityBasedFunctionFilterExpressionBuilderStrategy(this, 2);
			_strategies[FilterFunctions.Substring] = new ArityBasedFunctionFilterExpressionBuilderStrategy(this, 2);
			_strategies[FilterFunctions.SubstringOf] = new ArityBasedFunctionFilterExpressionBuilderStrategy(this, 2);

			_strategies[FilterFunctions.Replace] = new ArityBasedFunctionFilterExpressionBuilderStrategy(this, 3);
		}

		public string BuildExpression(Stack<string> stack)
		{
			var item = stack.Peek();

			IODataFilterExpressionBuilderStrategy strategy;
			var result = _strategies.TryGetValue(item, out strategy)
				? strategy.BuildExpression(stack)
				: stack.Pop();

			return result;
		}
	}
}
