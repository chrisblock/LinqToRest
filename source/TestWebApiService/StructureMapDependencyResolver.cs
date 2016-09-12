using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;

using StructureMap;

namespace TestWebApiService
{
	public class StructureMapDependencyResolver : IDependencyResolver
	{
		private readonly IContainer _container;

		public StructureMapDependencyResolver(IContainer container)
		{
			_container = container;
		}

		public object GetService(Type serviceType)
		{
			object result;

			if (serviceType.IsInterface || serviceType.IsAbstract)
			{
				result = _container.TryGetInstance(serviceType);
			}
			else
			{
				result = _container.GetInstance(serviceType);
			}

			return result;
		}

		public IEnumerable<object> GetServices(Type serviceType)
		{
			return _container.GetAllInstances(serviceType)
				.Cast<object>();
		}

		public IDependencyScope BeginScope()
		{
			return new StructureMapDependencyScope(_container.GetNestedContainer());
		}

		~StructureMapDependencyResolver()
		{
			Dispose(false);

			GC.SuppressFinalize(this);
		}

		public void Dispose()
		{
			Dispose(true);
		}

		private void Dispose(bool disposing)
		{
			if (disposing)
			{
				// dispose managed resources

				_container.Dispose();
			}

			// dispose native resources
		}
	}
}
