using System;
using System.Collections.Generic;
using System.Linq;

using LinqToRest.OData.Filters;
using LinqToRest.OData.Lexing;

namespace LinqToRest.OData.Parsing
{
	public abstract class AbstractFilterExpressionParserStrategy<T> : IFilterExpressionParserStrategy
	{
		protected Type Type { get { return typeof (T); } }

		protected abstract T Parse(string text);

		public FilterExpression BuildExpression(Stack<Token> stack)
		{
			if (stack == null)
			{
				throw new ArgumentNullException("stack", "Cannot parse expression from null expression stack.");
			}

			if (stack.Any() == false)
			{
				throw new ArgumentException("Cannot parse expression from empty expression stack.", "stack");
			}

			var token = stack.Pop();

			if (token == null)
			{
				throw new ArgumentException("Cannot parse a null token.");
			}

			var value = Parse(token.Value);
			var type = typeof(T);

			return FilterExpression.Constant(value, type);
		}
	}
}
