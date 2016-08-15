using System;

using LinqToRest.Client.Http;
using LinqToRest.Client.Http.Impl;
using LinqToRest.Client.Linq;
using LinqToRest.Client.Serialization;
using LinqToRest.Client.Serialization.Impl;
using LinqToRest.OData;
using LinqToRest.OData.Impl;

namespace LinqToRest.Client
{
	public class EndpointBuilder : IEndpointBuilder
	{
		private readonly Uri _uri;
		private IHttpClientFactory _clientFactory;
		private IODataQueryFactory _oDataQueryFactory;
		private ISerializer _serializer;

		public EndpointBuilder(string url) : this(new Uri(url))
		{
		}

		public EndpointBuilder(Uri uri)
		{
			_uri = uri;
		}

		public IEndpointBuilder WithClientFactory(IHttpClientFactory clientFactory)
		{
			_clientFactory = clientFactory;

			return this;
		}

		public IEndpointBuilder WithSerializer(ISerializer serializer)
		{
			_serializer = serializer;

			return this;
		}

		public IEndpointBuilder WithODataQueryFactory(IODataQueryFactory oDataQueryFactory)
		{
			_oDataQueryFactory = oDataQueryFactory;

			return this;
		}

		public Endpoint Build()
		{
			var clientFactory = _clientFactory ?? new DefaultHttpClientFactory();
			var serializer = _serializer ?? new JsonSerializer();
			var oDataQueryFactory = _oDataQueryFactory ?? new DefaultODataQueryFactory();

			var uriFactory = new UriFactory(_uri);
			var httpService = new HttpService(clientFactory, serializer);
			var restQueryableFactory = new RestQueryableFactory(httpService, new RestQueryModelVisitor(oDataQueryFactory));

			return new Endpoint(restQueryableFactory, httpService, uriFactory);
		}
	}
}
