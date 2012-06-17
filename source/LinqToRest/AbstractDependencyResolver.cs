using System;
using System.Collections.Generic;

namespace LinqToRest
{
	internal abstract class AbstractDependencyResolver : IDependencyResolver
	{
		private readonly IDictionary<Type, Type> _concreteTypes = new Dictionary<Type, Type>();

		protected void Register<TInterface, TImplementation>() where TImplementation : TInterface
		{
			var interfaceType = typeof (TInterface);
			var concreteType = typeof (TImplementation);

			_concreteTypes.Add(interfaceType, concreteType);
		}

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
	}
}
