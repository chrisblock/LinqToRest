using System;
using System.Collections.Generic;

using LinqToRest.Http;
using LinqToRest.Http.Impl;
using LinqToRest.OData;
using LinqToRest.OData.Building.Strategies;
using LinqToRest.OData.Building.Strategies.Impl;
using LinqToRest.OData.Impl;
using LinqToRest.OData.Parsing;
using LinqToRest.OData.Parsing.Impl;
using LinqToRest.Serialization;
using LinqToRest.Serialization.Impl;

namespace LinqToRest
{
	internal class DefaultDependencyResolver : IDependencyResolver
	{
		private readonly IDictionary<Type, Type> _concreteTypes = new Dictionary<Type, Type>
		{
			{typeof(IHttpService), typeof(HttpService)},
			{typeof(ISerializer), typeof(JsonSerializer)},
			{typeof(IODataFilterExpressionBuilderStrategy), typeof(ODataFilterExpressionBuilderStrategy)},
			{typeof(IODataQueryParserStrategy), typeof(ODataQueryParserStrategy)},
			{typeof(ICompleteODataQueryFactory), typeof(DefaultCompleteODataQueryFactory)}
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
	}
}
