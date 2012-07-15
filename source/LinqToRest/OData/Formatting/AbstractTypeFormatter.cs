using System;

namespace LinqToRest.OData.Formatting
{
	public abstract class AbstractTypeFormatter<T> : ITypeFormatter
	{
		protected abstract string Format(T value);

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

			return Format((T) value);
		}
	}
}
