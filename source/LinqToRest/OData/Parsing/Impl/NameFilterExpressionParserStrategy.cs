using System;
using System.Collections.Generic;

using LinqToRest.OData.Filters;
using LinqToRest.OData.Lexing;

namespace LinqToRest.OData.Parsing.Impl
{
	public class NameFilterExpressionParserStrategy : IFilterExpressionParserStrategy
	{
		public FilterExpression BuildExpression(Stack<Token> stack)
		{
			if (stack == null)
			{
				throw new ArgumentNullException("stack", "Cannot build expression from null expression stack.");
			}

			if (stack.Count < 1)
			{
				throw new ArgumentException("Cannot build expression from empty expression stack.", "stack");
			}

			var token = stack.Pop();

			if (token == null)
			{
				throw new ArgumentException("Cannot build expression from null token.");
			}

			return FilterExpression.MemberAccess(token.Value);
		}
	}
}
