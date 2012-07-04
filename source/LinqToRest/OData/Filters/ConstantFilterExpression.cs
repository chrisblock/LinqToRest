using System;

using LinqToRest.OData.Formatting;
using LinqToRest.OData.Formatting.Impl;

namespace LinqToRest.OData.Filters
{
	public class ConstantFilterExpression : FilterExpression
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

		public override string ToString()
		{
			var result = (Value == null)
				? "Null"
				: Formatter.Format(Type, Value);

			return result;
		}
	}
}
