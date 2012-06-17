using System;
using System.Collections.Generic;

namespace LinqToRest.OData.Filters
{
	public class ConstantFilterExpression : FilterExpression
	{
		private static readonly IDictionary<Type, Func<object, string>> TypeFormatters = new Dictionary<Type, Func<object, string>>
		{
			{ typeof(string), obj => String.Format("'{0}'", obj) },
			{ typeof(Guid), obj => String.Format("guid'{0}'", obj) },
			{ typeof(DateTime), obj => String.Format("datetime'{0:yyyy-MM-ddTHH:mm:ssK}'", obj) },
			{ typeof(TimeSpan), obj => String.Format("time'{0}'", obj) },
			{ typeof(DateTimeOffset), obj => String.Format("datetimeoffset'{0}'", obj) },
			{ typeof(decimal), obj => String.Format("{0}m", obj) }
		};

		public override FilterExpressionType ExpressionType { get { return FilterExpressionType.Constant; } }

		public object Value { get; private set; }

		public Type Type { get; private set; }

		public ConstantFilterExpression(object value, Type type = null)
		{
			var valueType = type;

			if ((value != null) && (valueType == null))
			{
				valueType = value.GetType();
			}

			Value = value;
			Type = valueType;
		}

		public override string ToString()
		{
			string result;

			if (Value == null)
			{
				result = "Null";
			}
			else
			{
				var t = Type ?? Value.GetType();

				Func<object, string> formatter;
				if (TypeFormatters.TryGetValue(t, out formatter) == false)
				{
					formatter = obj => String.Format("{0}", obj);
				}

				result = formatter(Value);
			}

			return result;
		}
	}
}
