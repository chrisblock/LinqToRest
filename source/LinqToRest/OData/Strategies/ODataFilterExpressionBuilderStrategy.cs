using System.Collections.Generic;

namespace LinqToRest.OData.Strategies
{
	public class ODataFilterExpressionBuilderStrategy: IODataFilterExpressionBuilderStrategy
	{
		private readonly IDictionary<string, IODataFilterExpressionBuilderStrategy> _strategies = new Dictionary<string, IODataFilterExpressionBuilderStrategy>();

		public ODataFilterExpressionBuilderStrategy()
		{
			_strategies["->"] = new PropertyAccessFilterExpressionBuilderStrategy(this);

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

			_strategies["not"] = new StandardUnaryFilterExpressionBuilderStrategy(this);
			_strategies["-"] = new StandardUnaryFilterExpressionBuilderStrategy(this);

			_strategies["toupper"] = new ArityBasedFunctionFilterExpressionBuilderStrategy(this, 1);
			_strategies["tolower"] = new ArityBasedFunctionFilterExpressionBuilderStrategy(this, 1);
			_strategies["trim"] = new ArityBasedFunctionFilterExpressionBuilderStrategy(this, 1);
			_strategies["length"] = new ArityBasedFunctionFilterExpressionBuilderStrategy(this, 1);
			_strategies["year"] = new ArityBasedFunctionFilterExpressionBuilderStrategy(this, 1);
			_strategies["month"] = new ArityBasedFunctionFilterExpressionBuilderStrategy(this, 1);
			_strategies["day"] = new ArityBasedFunctionFilterExpressionBuilderStrategy(this, 1);
			_strategies["hour"] = new ArityBasedFunctionFilterExpressionBuilderStrategy(this, 1);
			_strategies["minute"] = new ArityBasedFunctionFilterExpressionBuilderStrategy(this, 1);
			_strategies["second"] = new ArityBasedFunctionFilterExpressionBuilderStrategy(this, 1);
			_strategies["ceiling"] = new ArityBasedFunctionFilterExpressionBuilderStrategy(this, 1);
			_strategies["floor"] = new ArityBasedFunctionFilterExpressionBuilderStrategy(this, 1);
			_strategies["round"] = new ArityBasedFunctionFilterExpressionBuilderStrategy(this, 1);

			_strategies["isof"] = new ArityBasedFunctionFilterExpressionBuilderStrategy(this, 2);
			_strategies["cast"] = new ArityBasedFunctionFilterExpressionBuilderStrategy(this, 2);
			_strategies["indexof"] = new ArityBasedFunctionFilterExpressionBuilderStrategy(this, 2);
			_strategies["startswith"] = new ArityBasedFunctionFilterExpressionBuilderStrategy(this, 2);
			_strategies["endswith"] = new ArityBasedFunctionFilterExpressionBuilderStrategy(this, 2);
			_strategies["substring"] = new ArityBasedFunctionFilterExpressionBuilderStrategy(this, 2);
			_strategies["substringof"] = new ArityBasedFunctionFilterExpressionBuilderStrategy(this, 2);

			_strategies["replace"] = new ArityBasedFunctionFilterExpressionBuilderStrategy(this, 3);
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
