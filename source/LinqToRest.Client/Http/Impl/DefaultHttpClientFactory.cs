using System.Net.Http;
using System.Net.Http.Headers;

namespace LinqToRest.Client.Http.Impl
{
	public class DefaultHttpClientFactory : IHttpClientFactory
	{
		public HttpClient CreateFor(HttpVerb verb)
		{
			var client = new HttpClient();

			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

			return client;
		}
	}
}
