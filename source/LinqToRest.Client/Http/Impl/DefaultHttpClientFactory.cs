using System.Net.Http;

namespace LinqToRest.Client.Http.Impl
{
	public class DefaultHttpClientFactory : IHttpClientFactory
	{
		public HttpClient CreateFor(HttpVerb verb)
		{
			var client = new HttpClient();

			client.DefaultRequestHeaders.Add("Accept", "application/json");

			return client;
		}
	}
}
