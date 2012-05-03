using System;
using System.Collections.Generic;

namespace LinqToRest.OData.Filters
{
	public class ODataQueryConstantFilterExpression : ODataQueryFilterExpression
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

		public override ODataQueryFilterExpressionType ExpressionType { get { return ODataQueryFilterExpressionType.Constant; } }

		public object Value { get; private set; }

		public Type Type { get; private set; }

		public ODataQueryConstantFilterExpression(object value, Type type = null)
		{
			Value = value;
			Type = type;
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
