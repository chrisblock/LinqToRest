using System;

namespace LinqToRest.OData
{
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
	public class UrlParameterAttribute : Attribute
	{
		public string Name { get; private set; }

		public UrlParameterAttribute(string parameterName)
		{
			Name = parameterName;
		}
	}
}
