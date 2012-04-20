using System.Collections.Generic;

namespace LinqToRest.OData.Strategies
{
	public interface IODataFilterExpressionBuilderStrategy
	{
		string BuildExpression(Stack<string> stack);
	}
}
