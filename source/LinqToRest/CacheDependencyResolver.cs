using System;
using System.Collections.Concurrent;

namespace LinqToRest
{
	internal class CacheDependencyResolver : IDependencyResolver
	{
		private readonly ConcurrentDictionary<Type, object> _instanceCache = new ConcurrentDictionary<Type, object>();

		private readonly IDependencyResolver _resolver;

		public CacheDependencyResolver(IDependencyResolver resolver)
		{
			_resolver = resolver;
		}

		public object GetInstance(Type type)
		{
			return _instanceCache.GetOrAdd(type, _resolver.GetInstance);
		}
	}
}
