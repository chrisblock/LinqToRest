using System;
using System.Collections.Generic;

namespace LinqToRest
{
	public interface IDependencyResolver
	{
		object GetInstance(Type type);
		IEnumerable<object> GetAllInstances(Type type);
	}
}
