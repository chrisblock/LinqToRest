using System;

namespace LinqToRest.OData.Formatting.Impl
{
	public class DefaultFormatter : ITypeFormatter
	{
		public string Format(Type type, object value)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}

			if (value == null)
			{
				throw new ArgumentNullException("value");
			}

			return String.Format("{0}", value);
		}
	}
}
