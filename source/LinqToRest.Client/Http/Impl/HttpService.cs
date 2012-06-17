using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace LinqToRest.Client.Http.Impl
{
	public class HttpService : IHttpService
	{
		private readonly IHttpClientFactory _httpClientFactory;

		public HttpService() : this(DependencyResolver.Current.GetInstance<IHttpClientFactory>())
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

				resultTask.Wait();

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

		public void Put(string url, HttpContent content)
		{
			var uri = new Uri(url);

			Put(uri, content);
		}

		public void Put(Uri uri, HttpContent content)
		{
			PerformHttpOperation(client => client.PutAsync(uri, content));
		}

		public void Post(string url, HttpContent content)
		{
			var uri = new Uri(url);

			Post(uri, content);
		}

		public void Post(Uri uri, HttpContent content)
		{
			PerformHttpOperation(client => client.PostAsync(uri, content));
		}

		public void Delete(string url)
		{
			var uri = new Uri(url);

			Delete(uri);
		}

		public void Delete(Uri uri)
		{
			PerformHttpOperation(client => client.DeleteAsync(uri));
		}
	}
}
