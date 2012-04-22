using System;
using System.Collections.Generic;
using System.Linq;

using LinqToRest.Http;
using LinqToRest.Http.Impl;
using LinqToRest.Linq;
using LinqToRest.Serialization;
using LinqToRest.Serialization.Impl;

using Remotion.Linq;

namespace LinqToRest
{
	internal class DefaultDependencyResolver : IDependencyResolver
	{
		private readonly IDictionary<Type, Type> _concreteTypes = new Dictionary<Type, Type>
		{
			{typeof(IHttpService), typeof(HttpService)},
			{typeof(ISerializer), typeof(JsonSerializer)},
			{typeof(IQueryExecutor), typeof(RestQueryExecutor)}
		};

		public object GetInstance(Type type)
		{
			object result = null;

			if (_concreteTypes.ContainsKey(type))
			{
				type = _concreteTypes[type];
			}


			if ((type.IsAbstract == false) && (type.IsInterface == false))
			{
				try
				{
					result = Activator.CreateInstance(type);
				}
				catch
				{
				}
			}

			return result;
		}

		public IEnumerable<object> GetAllInstances(Type type)
		{
			return Enumerable.Empty<object>();
		}
	}
}
