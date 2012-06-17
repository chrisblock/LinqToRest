using System;

namespace LinqToRest.OData.Filters
{
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
	public class FilterMethodAttribute : Attribute
	{
		public string Name { get; private set; }

		public FilterMethodAttribute(string oDataQueryMethodName)
		{
			Name = oDataQueryMethodName;
		}
	}
}
