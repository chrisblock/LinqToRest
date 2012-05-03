using System;

namespace LinqToRest.OData.Filters
{
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
	public class DotNetMethodAttribute : Attribute
	{
		public string Name { get; private set; }

		public DotNetMethodAttribute(string dotNetMethodName)
		{
			Name = dotNetMethodName;
		}
	}
}
