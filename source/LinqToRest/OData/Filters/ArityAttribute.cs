using System;

namespace LinqToRest.OData.Filters
{
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
	public class ArityAttribute : Attribute
	{
		public int Arity { get; private set; }

		public ArityAttribute(int arity)
		{
			Arity = arity;
		}
	}
}
