using System;

using LinqToRest.OData.Lexing;

namespace LinqToRest.OData.Formatting.Impl
{
	public class EdmTypeFormatter : AbstractTypeFormatter<Type>
	{
		protected override string Format(Type value)
		{
			return EdmTypeNames.Lookup(value);
		}
	}
}
