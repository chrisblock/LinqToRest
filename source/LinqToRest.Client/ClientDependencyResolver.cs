using LinqToRest.Client.Http;
using LinqToRest.Client.Http.Impl;
using LinqToRest.OData;
using LinqToRest.OData.Building.Strategies;
using LinqToRest.OData.Building.Strategies.Impl;
using LinqToRest.OData.Impl;
using LinqToRest.Serialization;
using LinqToRest.Serialization.Impl;

namespace LinqToRest.Client
{
	internal class ClientDependencyResolver : AbstractDependencyResolver
	{
		public ClientDependencyResolver()
		{
			Register<IHttpService, HttpService>();
			Register<ISerializer, JsonSerializer>();
			Register<IFilterExpressionBuilderStrategy, FilterExpressionBuilderStrategy>();
			Register<IODataQueryFactory, DefaultODataQueryFactory>();
			Register<IHttpClientFactory, DefaultHttpClientFactory>();
		}
	}
}
