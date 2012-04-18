using System;

namespace LinqToRest
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	public class ServiceUrlAttribute : Attribute
	{
		public string Url { get; private set; }

		// TODO: add defaulted Id property name argument??
		public ServiceUrlAttribute(string url) //, id = "Id");
		{
			Url = url;
		}
	}
}
