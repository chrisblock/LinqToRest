using System;

using LinqToRest.OData.Literals;

namespace LinqToRest.OData.Parsing.Impl
{
	public class PrimitiveLiteralTokenParser : AbstractLiteralTokenParser<Type>
	{
		protected override Type Parse(string text)
		{
			var result = EdmTypeNames.Lookup(text);

			return result;
		}
	}
}
