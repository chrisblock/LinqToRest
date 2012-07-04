using System;

namespace LinqToRest.OData.Formatting
{
	public interface ITypeFormatter
	{
		string Format(Type type, object value);
	}
}
