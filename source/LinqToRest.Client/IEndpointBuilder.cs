using LinqToRest.Client.Http;
using LinqToRest.OData;

namespace LinqToRest.Client
{
	public interface IEndpointBuilder
	{
		IEndpointBuilder WithClientFactory(IHttpClientFactory clientFactory);
		IEndpointBuilder WithODataQueryFactory(IODataQueryFactory oDataQueryFactory);
	}
}
