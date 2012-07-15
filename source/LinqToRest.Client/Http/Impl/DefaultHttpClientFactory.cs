using System;
using System.Net.Http;

namespace LinqToRest.Client.Http.Impl
{
	public class DefaultHttpClientFactory : IHttpClientFactory
	{
		public HttpClient CreateFor(HttpVerb verb)
		{
			var client = new HttpClient();

			switch (verb)
			{
				case HttpVerb.Get:
					client.DefaultRequestHeaders.Add("Accept", "application/json");
					break;
				case HttpVerb.Put:
					client.DefaultRequestHeaders.Add("Accept", "application/json");
					break;
				case HttpVerb.Post:
					client.DefaultRequestHeaders.Add("Accept", "application/json");
					break;
				case HttpVerb.Delete:
					client.DefaultRequestHeaders.Add("Accept", "application/json");
					break;
				default:
					throw new ArgumentOutOfRangeException("verb");
			}

			return client;
		}
	}
}
