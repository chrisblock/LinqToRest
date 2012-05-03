using System;

namespace LinqToRest.OData.Filters
{
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
	public class ODataQueryOperatorAttribute : Attribute
	{
		public string Value { get; private set; }

		public ODataQueryOperatorAttribute(string value)
		{
			Value = value;
		}
	}
}
