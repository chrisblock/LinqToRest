using System;
using System.Linq;
using System.Text.RegularExpressions;

using LinqToRest.OData.Filters;
using LinqToRest.OData.Literals;

namespace LinqToRest.OData.Parsing.Impl
{
	public class NullLiteralTokenParser : ILiteralTokenParser
	{
		private static readonly Regex TypeSpecified = new Regex(@"^null'([^']+)'");

		public FilterExpression Parse(Token token)
		{
			if (token == null)
			{
				throw new ArgumentNullException("token", "Cannot parse null literal token.");
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
