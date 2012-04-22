using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace LinqToRest
{
	internal class CacheDependencyResolver : IDependencyResolver
	{
		private readonly ConcurrentDictionary<Type, object> _instanceCache = new ConcurrentDictionary<Type, object>();
		private readonly ConcurrentDictionary<Type, IEnumerable<object>> _instancesCache = new ConcurrentDictionary<Type, IEnumerable<object>>();

		private readonly IDependencyResolver _resolver;

		public CacheDependencyResolver(IDependencyResolver resolver)
		{
			_resolver = resolver;
		}

		public object GetInstance(Type type)
		{
			return _instanceCache.GetOrAdd(type, _resolver.GetInstance);
		}

		public IEnumerable<object> GetAllInstances(Type type)
		{
			return _instancesCache.GetOrAdd(type, _resolver.GetAllInstances);
		}
	}
}
