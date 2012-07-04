using System;

namespace LinqToRest.OData.Formatting.Impl
{
	public class DefaultFormatter : ITypeFormatter
	{
		public string Format(Type type, object value)
		{
			return String.Format("{0}", value);
		}
	}
}
