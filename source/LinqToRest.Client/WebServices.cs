using System;

namespace LinqToRest.Client
{
	public static class WebServices
	{
		public static Endpoint CreateEndpoint(string url)
		{
			if (String.IsNullOrWhiteSpace(url) || (url.EndsWith("/") == false))
			{
				throw new ArgumentException($"URL '{url}' does not end with '/'. This will cause errors in future processing, and is therefore disallowed.");
			}

			return CreateEndpoint(new Uri(url));
		}

		public static Endpoint CreateEndpoint(Uri uri)
		{
			return CreateEndpoint(uri, x => { });
		}

		public static Endpoint CreateEndpoint(string url, Action<IEndpointBuilder> configure)
		{
			return CreateEndpoint(new Uri(url), configure);
		}

		public static Endpoint CreateEndpoint(Uri uri, Action<IEndpointBuilder> configure)
		{
			var builder = new EndpointBuilder(uri);

			configure(builder);

			return builder.Build();
		}
	}
}
