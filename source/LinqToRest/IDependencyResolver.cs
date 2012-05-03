using System;

namespace LinqToRest
{
	public interface IDependencyResolver
	{
		object GetInstance(Type type);
	}
}
