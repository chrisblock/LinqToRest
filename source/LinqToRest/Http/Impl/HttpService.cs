using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace LinqToRest.Http.Impl
{
	public class HttpService : IHttpService
	{
		private readonly IHttpClientFactory _httpClientFactory;

		public HttpService() : this(DependencyResolver.Current.GetInstance<DefaultHttpClientFactory>())
		{
		}

		public HttpService(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}

		private string PerformHttpOperation(Func<HttpClient, Task<HttpResponseMessage>> asyncHttpMethod)
		{
			string result;

			using (var httpClient = _httpClientFactory.Create())
			{
				var response = asyncHttpMethod(httpClient);

				response.Wait();

				var responseMessage = response.Result;

				responseMessage.EnsureSuccessStatusCode();

				var resultTask = responseMessage.Content.ReadAsStringAsync();

				result = resultTask.Result;
			}

			return result;
		}

		public string Get(string url)
		{
			var uri = new Uri(url);

			return Get(uri);
		}

		public string Get(Uri uri)
		{
			var result = PerformHttpOperation(client => client.GetAsync(uri));

			return result;
		}
	}
}
