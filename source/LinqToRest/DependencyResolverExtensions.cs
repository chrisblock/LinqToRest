using System.Collections.Generic;
using System.Linq;

namespace LinqToRest
{
	public static class DependencyResolverExtensions
	{
		public static T GetInstance<T>(this IDependencyResolver dependencyResolver)
		{
			return (T) dependencyResolver.GetInstance(typeof (T));
		}

		public static IEnumerable<T> GetAllInstances<T>(this IDependencyResolver dependencyResolver)
		{
			return dependencyResolver.GetAllInstances(typeof (T)).Cast<T>();
		}
	}
}
