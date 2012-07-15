using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

using LinqToRest.OData.Filters;
using LinqToRest.OData.Lexing;

namespace LinqToRest.OData.Parsing.Impl
{
	public class NullFilterExpressionParserStrategy : IFilterExpressionParserStrategy
	{
		private static readonly Regex TypeSpecified = new Regex(@"^null'([^']+)'", RegexOptions.IgnoreCase);

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

			ConstantFilterExpression result;

			var match = TypeSpecified.Match(token.Value);

			if (match.Success)
			{
				// TODO: need to support more than just the EDM type primitives defined in the OData ABNF
				var typeName = match.Groups.Cast<Group>().Skip(1).Single()
					.Captures.Cast<Capture>().Single().Value;

				var type = EdmTypeNames.Lookup(typeName);

				result = FilterExpression.Constant(null, type);
			}
			else
			{
				result = FilterExpression.Constant<object>(null);
			}

			return result;
		}
	}
}
