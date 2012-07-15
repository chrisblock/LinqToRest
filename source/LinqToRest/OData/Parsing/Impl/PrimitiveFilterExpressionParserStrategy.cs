using System;

using LinqToRest.OData.Lexing;

namespace LinqToRest.OData.Parsing.Impl
{
	public class PrimitiveFilterExpressionParserStrategy : AbstractFilterExpressionParserStrategy<Type>
	{
		protected override Type Parse(string text)
		{
			var result = EdmTypeNames.Lookup(text);

			if (result == null)
			{
				throw new ArgumentException(String.Format("'{0}' is not recognized as an OData specified primitive type.", text));
			}

			return result;
		}
	}
}
