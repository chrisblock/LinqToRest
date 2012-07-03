using System.Net.Http;

namespace LinqToRest.Client.Http
{
	public interface IHttpClientFactory
	{
		HttpClient CreateFor(HttpVerb verb);
	}
}
