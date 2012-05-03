using System;

namespace LinqToRest.OData.Filters
{
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
	public class ODataQueryMethodAttribute : Attribute
	{
		public string Name { get; private set; }

		public ODataQueryMethodAttribute(string oDataQueryMethodName)
		{
			Name = oDataQueryMethodName;
		}
	}
}
