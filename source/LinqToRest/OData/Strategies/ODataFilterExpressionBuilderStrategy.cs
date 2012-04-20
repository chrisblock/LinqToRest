using System.Collections.Generic;

namespace LinqToRest.OData.Strategies
{
	public class ODataFilterExpressionBuilderStrategy: IODataFilterExpressionBuilderStrategy
	{
		private readonly IDictionary<string, IODataFilterExpressionBuilderStrategy> _strategies = new Dictionary<string, IODataFilterExpressionBuilderStrategy>();

		public ODataFilterExpressionBuilderStrategy()
		{
			_strategies["add"] = new StandardBinaryFilterExpressionBuilderStrategy(this);
			_strategies["and"] = new StandardBinaryFilterExpressionBuilderStrategy(this);
			_strategies["div"] = new StandardBinaryFilterExpressionBuilderStrategy(this);
			_strategies["eq"] = new StandardBinaryFilterExpressionBuilderStrategy(this);
			_strategies["gt"] = new StandardBinaryFilterExpressionBuilderStrategy(this);
			_strategies["ge"] = new StandardBinaryFilterExpressionBuilderStrategy(this);
			_strategies["lt"] = new StandardBinaryFilterExpressionBuilderStrategy(this);
			_strategies["le"] = new StandardBinaryFilterExpressionBuilderStrategy(this);
			_strategies["mod"] = new StandardBinaryFilterExpressionBuilderStrategy(this);
			_strategies["mul"] = new StandardBinaryFilterExpressionBuilderStrategy(this);
			_strategies["ne"] = new StandardBinaryFilterExpressionBuilderStrategy(this);
			_strategies["or"] = new StandardBinaryFilterExpressionBuilderStrategy(this);
			_strategies["sub"] = new StandardBinaryFilterExpressionBuilderStrategy(this);

			_strategies["isof"] = new TwoParameterFunctionBinaryFilterExpressionBuilderStrategy(this);
			_strategies["cast"] = new TwoParameterFunctionBinaryFilterExpressionBuilderStrategy(this);

			_strategies["not"] = new StandardUnaryFilterExpressionBuilderStrategy();
			_strategies["-"] = new StandardUnaryFilterExpressionBuilderStrategy();
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
