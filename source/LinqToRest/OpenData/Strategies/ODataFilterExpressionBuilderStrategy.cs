using System.Collections.Generic;

namespace LinqToRest.OpenData.Strategies
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
		}

		public string BuildExpression(Stack<string> stack)
		{
			var item = stack.Peek();
			string result;

			IODataFilterExpressionBuilderStrategy strategy;
			if (_strategies.TryGetValue(item, out strategy))
			{
				result = strategy.BuildExpression(stack);
			}
			else
			{
				result = stack.Pop();
			}

			return result;
		}
	}
}
