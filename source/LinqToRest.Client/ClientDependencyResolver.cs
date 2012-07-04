using LinqToRest.Client.Http;
using LinqToRest.Client.Http.Impl;
using LinqToRest.OData;
using LinqToRest.OData.Impl;
using LinqToRest.OData.Parsing;
using LinqToRest.OData.Parsing.Impl;
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
			Register<IFilterExpressionParserStrategy, FilterExpressionParserStrategy>();
			Register<IODataQueryFactory, DefaultODataQueryFactory>();
			Register<IHttpClientFactory, DefaultHttpClientFactory>();
		}
	}
}
