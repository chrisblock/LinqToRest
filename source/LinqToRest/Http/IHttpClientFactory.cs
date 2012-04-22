using System.Net.Http;

namespace LinqToRest.Http
{
	public interface IHttpClientFactory
	{
		HttpClient Create();
	}
}
