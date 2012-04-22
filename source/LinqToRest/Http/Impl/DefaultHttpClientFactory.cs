using System.Net.Http;

namespace LinqToRest.Http.Impl
{
	public class DefaultHttpClientFactory : IHttpClientFactory
	{
		public HttpClient Create()
		{
			var client = new HttpClient();
			client.DefaultRequestHeaders.Add("Accept", "application/json");

			return client;
		}
	}
}
