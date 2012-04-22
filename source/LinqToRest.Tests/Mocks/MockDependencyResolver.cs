using System;
using System.Collections.Generic;

using LinqToRest.Http;

namespace LinqToRest.Tests.Mocks
{
	public class MockDependencyResolver : IDependencyResolver
	{
		private readonly IDependencyResolver _old;

		public MockDependencyResolver()
		{
			_old = DependencyResolver.Current;
		}

		public object GetInstance(Type type)
		{
			var result = type == typeof(IHttpService)
				? new MockHttpService()
				: _old.GetInstance(type);

			return result;
		}

		public IEnumerable<object> GetAllInstances(Type type)
		{
			return _old.GetAllInstances(type);
		}
	}
}
