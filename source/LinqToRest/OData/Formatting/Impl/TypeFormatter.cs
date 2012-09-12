using System;
using System.Collections.Generic;

namespace LinqToRest.OData.Formatting.Impl
{
	public class TypeFormatter : ITypeFormatter
	{
		private readonly ITypeFormatter _defaultFormatter;
		private readonly IDictionary<Type, ITypeFormatter> _formatters;

		public TypeFormatter()
		{
			_defaultFormatter = new DefaultFormatter();

			_formatters = new Dictionary<Type, ITypeFormatter>
			{
				{ typeof (string), new StringFormatter() },
				{ typeof (decimal), new DecimalFormatter() },
				{ typeof (float), new FloatFormatter() },
				{ typeof (long), new LongFormatter() },
				{ typeof (Type), new EdmTypeFormatter() },
				{ typeof (DateTime), new DateTimeFormatter() },
				{ typeof (DateTimeOffset), new DateTimeOffsetFormatter() },
				{ typeof (TimeSpan), new TimeFormatter() },
				{ typeof (Guid), new GuidFormatter() },
			};
		}

		public string Format(Type type, object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value", "Cannot format null value.");
			}

			ITypeFormatter formatter;
			if (_formatters.TryGetValue(type, out formatter) == false)
			{
				formatter = _defaultFormatter;
			}

			var result = formatter.Format(type, value);

			return result;
		}
	}
}
