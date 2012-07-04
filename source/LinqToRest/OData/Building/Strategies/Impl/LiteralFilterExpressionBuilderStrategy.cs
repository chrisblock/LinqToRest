using System;
using System.Collections.Generic;

using LinqToRest.OData.Filters;
using LinqToRest.OData.Literals;
using LinqToRest.OData.Parsing;
using LinqToRest.OData.Parsing.Impl;

namespace LinqToRest.OData.Building.Strategies.Impl
{
	public class LiteralFilterExpressionBuilderStrategy : IFilterExpressionBuilderStrategy
	{
		private readonly ILiteralTokenParser _parser;

		public LiteralFilterExpressionBuilderStrategy()
		{
			_parser = new LiteralTokenParser();
		}

		public FilterExpression BuildExpression(Stack<Token> stack)
		{
			if (stack == null)
			{
				throw new ArgumentNullException("stack", "Cannot build literal expression with a null expression stack.");
			}

			if (stack.Count < 1)
			{
				throw new ArgumentException("Cannot build literal expression with an empty expression stack.");
			}

			var token = stack.Pop();

			var result = _parser.Parse(token);

			return result;
		}
	}
}
