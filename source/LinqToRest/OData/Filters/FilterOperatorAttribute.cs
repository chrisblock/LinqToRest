using System;

namespace LinqToRest.OData.Filters
{
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
	public class FilterOperatorAttribute : Attribute
	{
		public string Value { get; private set; }

		public FilterOperatorAttribute(string value)
		{
			Value = value;
		}
	}
}
