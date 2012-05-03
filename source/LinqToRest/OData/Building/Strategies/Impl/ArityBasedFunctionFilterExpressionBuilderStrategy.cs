using System.Collections.Generic;

using LinqToRest.OData.Filters;

namespace LinqToRest.OData.Building.Strategies.Impl
{
	public class ArityBasedFunctionFilterExpressionBuilderStrategy : IODataFilterExpressionBuilderStrategy
	{
		private readonly IODataFilterExpressionBuilderStrategy _oDataFilterExpressionBuilderStrategy;
		private readonly int _arity;

		public ArityBasedFunctionFilterExpressionBuilderStrategy(IODataFilterExpressionBuilderStrategy oDataFilterExpressionBuilderStrategy, int arity)
		{
			_oDataFilterExpressionBuilderStrategy = oDataFilterExpressionBuilderStrategy;
			_arity = arity;
		}

		public ODataQueryFilterExpression BuildExpression(Stack<string> stack)
		{
			var function = stack.Pop().GetFromODataQueryMethodName();
			var arguments = new List<ODataQueryFilterExpression>();

			for (int i = 0; i < _arity; i++)
			{
				var argument = _oDataFilterExpressionBuilderStrategy.BuildExpression(stack);
				arguments.Add(argument);
			}

			arguments.Reverse();

			var result = ODataQueryFilterExpression.MethodCall(function, arguments.ToArray());

			return result;
		}
	}
}
