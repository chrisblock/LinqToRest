using System;

using LinqToRest.Client.Http.Impl;
using LinqToRest.Client.Linq;
using LinqToRest.Client.Serialization.Impl;
using LinqToRest.OData.Impl;

namespace LinqToRest.Client
{
	public static class WebServices
	{
		public static Endpoint CreateEndpoint(string url)
		{
			if (String.IsNullOrWhiteSpace(url) || (url.EndsWith("/") == false))
			{
				throw new ArgumentException(String.Format("URL '{0}' does not end with '/'. This will cause errors in future processing, and is therefore disallowed.", url));
			}

			return CreateEndpoint(new Uri(url));
		}

		public static Endpoint CreateEndpoint(Uri uri)
		{
			// TODO: figure out how to allow the injection of these things without ridiculous DependencyResolver shenanigans
			var httpClientFactory = new DefaultHttpClientFactory();
			var serializer = new JsonSerializer();
			var oDataQueryFactory = new DefaultODataQueryFactory();

			var uriFactory = new UriFactory(uri);

			var httpService = new HttpService(httpClientFactory, serializer);
			var queryModelTranslator = new RestQueryModelVisitor(uriFactory, oDataQueryFactory);
			var restQueryableFactory = new RestQueryableFactory(httpService, queryModelTranslator);

			return new Endpoint(restQueryableFactory, httpService, uriFactory);
		}
	}
}
