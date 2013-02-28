using System;

using LinqToRest.OData.Formatting;
using LinqToRest.OData.Formatting.Impl;

namespace LinqToRest.OData.Filters
{
	public class ConstantFilterExpression : FilterExpression, IEquatable<ConstantFilterExpression>
	{
		private static readonly ITypeFormatter Formatter = new TypeFormatter();

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

		public bool Equals(ConstantFilterExpression other)
		{
			var result = false;

			if (ReferenceEquals(null, other))
			{
				result = false;
			}
			else if (ReferenceEquals(this, other))
			{
				result = true;
			}
			else
			{
				result = Equals(other.Type, Type) && Equals(other.Value, Value);
			}

			return result;
		}

		public override bool Equals(object obj)
		{
			var result = false;

			if (ReferenceEquals(null, obj))
			{
				result = false;
			}
			else if (ReferenceEquals(this, obj))
			{
				result = true;
			}
			else if (obj.GetType() != typeof (ConstantFilterExpression))
			{
				result = false;
			}
			else
			{
				result = Equals((ConstantFilterExpression) obj);
			}

			return result;
		}

		public override int GetHashCode()
		{
			var result = String.Format("Type:{0};Value:{1};", Type, Value);

			return result.GetHashCode();
		}

		public override string ToString()
		{
			var result = (Value == null)
				? "Null"
				: Formatter.Format(Type, Value);

			return result;
		}
	}
}
