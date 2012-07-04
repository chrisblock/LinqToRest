using System;

using LinqToRest.OData.Lexing;

namespace LinqToRest.OData.Parsing.Impl
{
	public class PrimitiveFilterExpressionParserStrategy : AbstractFilterExpressionParserStrategy<Type>
	{
		protected override Type Parse(string text)
		{
			var result = EdmTypeNames.Lookup(text);

			return result;
		}
	}
}
