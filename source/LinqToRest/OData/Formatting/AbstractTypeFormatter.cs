using System;

namespace LinqToRest.OData.Formatting
{
	public abstract class AbstractTypeFormatter<T> : ITypeFormatter
	{
		protected abstract string Format(T value);

		public string Format(Type type, object value)
		{
			return Format((T) value);
		}
	}
}
