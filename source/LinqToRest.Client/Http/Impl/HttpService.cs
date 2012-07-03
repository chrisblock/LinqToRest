using System;
using System.Net;
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

		private HttpStatusCode PerformHttpOperation<T>(HttpVerb httpVerb, Func<HttpClient, Task<HttpResponseMessage>> asyncHttpMethod, out T result)
		{
			HttpStatusCode status;

			using (var httpClient = _httpClientFactory.CreateFor(httpVerb))
			{
				var response = asyncHttpMethod(httpClient);

				response.Wait();

				if (response.IsFaulted || (response.Exception != null))
				{
					result = default(T);
					status = HttpStatusCode.BadRequest;
				}
				else
				{
					var responseMessage = response.Result;

					responseMessage.EnsureSuccessStatusCode();

					var resultTask = responseMessage.Content.ReadAsAsync<T>();

					resultTask.Wait();

					result = resultTask.Result;

					status = responseMessage.StatusCode;
				}
			}

			return status;
		}

		public T Get<T>(string url)
		{
			var uri = new Uri(url);

			return Get<T>(uri);
		}

		public T Get<T>(Uri uri)
		{
			T result;

			PerformHttpOperation(HttpVerb.Get, client => client.GetAsync(uri), out result);

			return result;
		}

		public HttpStatusCode Put(string url, HttpContent content)
		{
			var uri = new Uri(url);

			return Put(uri, content);
		}

		public HttpStatusCode Put(Uri uri, HttpContent content)
		{
			string result;
			return PerformHttpOperation(HttpVerb.Put, client => client.PutAsync(uri, content), out result);
		}

		public HttpStatusCode Post(string url, HttpContent content)
		{
			var uri = new Uri(url);

			return Post(uri, content);
		}

		public HttpStatusCode Post(Uri uri, HttpContent content)
		{
			string result;
			return PerformHttpOperation(HttpVerb.Post, client => client.PostAsync(uri, content), out result);
		}

		public HttpStatusCode Delete(string url)
		{
			var uri = new Uri(url);

			return Delete(uri);
		}

		public HttpStatusCode Delete(Uri uri)
		{
			string result;
			return PerformHttpOperation(HttpVerb.Delete, client => client.DeleteAsync(uri), out result);
		}
	}
}
