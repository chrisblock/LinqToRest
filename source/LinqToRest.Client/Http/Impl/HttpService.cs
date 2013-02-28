using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using Changes;

using LinqToRest.Client.Serialization;

namespace LinqToRest.Client.Http.Impl
{
	public class HttpService : IHttpService
	{
		private readonly IHttpClientFactory _httpClientFactory;
		private readonly ISerializer _serializer;

		public HttpService(IHttpClientFactory httpClientFactory, ISerializer serializer)
		{
			_httpClientFactory = httpClientFactory;
			_serializer = serializer;
		}

		private HttpStatusCode PerformHttpOperation(HttpVerb httpVerb, Func<HttpClient, Task<HttpResponseMessage>> asyncHttpMethod)
		{
			HttpStatusCode status;

			using (var httpClient = _httpClientFactory.CreateFor(httpVerb))
			{
				var response = asyncHttpMethod(httpClient);

				response.Wait();

				if (response.IsFaulted || (response.Exception != null))
				{
					status = HttpStatusCode.BadRequest;
				}
				else
				{
					var result = response.Result;

					status = result.StatusCode;
				}
			}

			return status;
		}

		private void PerformHttpOperation<T>(HttpVerb httpVerb, Func<HttpClient, Task<HttpResponseMessage>> asyncHttpMethod, out T result)
		{
			using (var httpClient = _httpClientFactory.CreateFor(httpVerb))
			{
				var response = asyncHttpMethod(httpClient);

				response.Wait();

				if (response.IsFaulted || (response.Exception != null))
				{
					result = default(T);
				}
				else
				{
					var responseMessage = response.Result;

					responseMessage.EnsureSuccessStatusCode();

					result = _serializer.Deserialize<T>(responseMessage.Content);
				}
			}
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

		public HttpStatusCode Put<T>(string url, ChangeSet<T> changes)
		{
			var uri = new Uri(url);

			return Put(uri, changes);
		}

		public HttpStatusCode Put<T>(Uri uri, ChangeSet<T> changes)
		{
			var content = _serializer.Serialize(changes);

			return PerformHttpOperation(HttpVerb.Put, client => client.PutAsync(uri, content));
		}

		public HttpStatusCode Post<T>(string url, T item)
		{
			var uri = new Uri(url);

			return Post(uri, item);
		}

		public HttpStatusCode Post<T>(Uri uri, T item)
		{
			var content = _serializer.Serialize(item);

			return PerformHttpOperation(HttpVerb.Post, client => client.PostAsync(uri, content));
		}

		public HttpStatusCode Delete(string url)
		{
			var uri = new Uri(url);

			return Delete(uri);
		}

		public HttpStatusCode Delete(Uri uri)
		{
			return PerformHttpOperation(HttpVerb.Delete, client => client.DeleteAsync(uri));
		}
	}
}
