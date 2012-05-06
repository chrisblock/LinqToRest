using System;

namespace LinqToRest.OData
{
	public class UrlParameterAttribute : Attribute
	{
		public string Name { get; private set; }

		public UrlParameterAttribute(string parameterName)
		{
			Name = parameterName;
		}
	}
}
