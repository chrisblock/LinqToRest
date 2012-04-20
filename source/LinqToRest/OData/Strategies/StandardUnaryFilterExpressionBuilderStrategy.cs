using System;
using System.Collections.Generic;

namespace LinqToRest.OData.Strategies
{
	public class StandardUnaryFilterExpressionBuilderStrategy : IODataFilterExpressionBuilderStrategy
	{
		public string BuildExpression(Stack<string> stack)
		{
			var unaryOperator = stack.Pop();
			var operand = stack.Pop();

			return String.Format("({0} {1})", unaryOperator, operand);
		}
	}
}
