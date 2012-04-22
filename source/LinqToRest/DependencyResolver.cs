using System;

namespace LinqToRest
{
	public static class DependencyResolver
	{
		private static readonly object ResolverLock = new object();

		static DependencyResolver()
		{
			Current = new CacheDependencyResolver(new DefaultDependencyResolver());
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
				Current = new CacheDependencyResolver(dependencyResolver);
			}
		}
	}
}
