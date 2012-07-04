using System;

using LinqToRest.OData.Literals;

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
