using System.Collections.Generic;

namespace LinqToRest.OpenData.Strategies
{
	public interface IODataFilterExpressionBuilderStrategy
	{
		string BuildExpression(Stack<string> stack);
	}
}
