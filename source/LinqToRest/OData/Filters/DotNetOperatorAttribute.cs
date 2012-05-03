using System;
using System.Linq.Expressions;

namespace LinqToRest.OData.Filters
{
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
	public class DotNetOperatorAttribute : Attribute
	{
		public ExpressionType ExpressionType { get; private set; }

		public DotNetOperatorAttribute(ExpressionType expressionType)
		{
			ExpressionType = expressionType;
		}
	}
}
