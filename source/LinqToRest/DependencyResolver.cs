using System;
using System.Linq;

namespace LinqToRest
{
	public static class DependencyResolver
	{
		private static readonly object ResolverLock = new object();

		static DependencyResolver()
		{
			var baseDependencyResolver = typeof (AbstractDependencyResolver);

			var assemblies = AppDomain.CurrentDomain.GetAssemblies()
				.Where(x => (x.GlobalAssemblyCache == false) && (x.IsDynamic == false));

			var dependencyResolvers = assemblies
				.SelectMany(x => x.GetTypes())
				.Where(x => x.IsAbstract == false)
				.Where(x => typeof (AbstractDependencyResolver).IsAssignableFrom(x))
				.ToList();

			if (dependencyResolvers.Count == 0)
			{
				throw new ApplicationException(String.Format("Cannot find an implementation of '{0}'.", baseDependencyResolver));
			}
			else if (dependencyResolvers.Count > 1)
			{
				throw new ApplicationException(String.Format("Found multiple implementations of '{0}'.", baseDependencyResolver));
			}

			var dependencyResolver = (IDependencyResolver) Activator.CreateInstance(dependencyResolvers.Single());

			Current = new CacheDependencyResolver(dependencyResolver);
		}

		public static IDependencyResolver Current { get; private set; }

		public static void SetDependencyResolver(IDependencyResolver dependencyResolver)
		{
			if (dependencyResolver == null)
			{
				throw new ArgumentNullException("dependencyResolver");
			}

			lock (ResolverLock)
			{
				Current = dependencyResolver;
			}
		}
	}
}
