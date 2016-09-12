using System;
using System.Net;
using System.Net.Http;

using Changes;

using LinqToRest.Client.Http;

using Microsoft.Owin.Testing;

namespace LinqToRest.IntegrationTests
{
	public class RequestBuilderHttpService : IHttpService
	{
		public TestServer TestServer { get; set; }

		public RequestBuilderHttpService(TestServer testServer)
		{
			TestServer = testServer;
		}

		public T Get<T>(string url)
		{
			var task = TestServer.CreateRequest(url)
				.GetAsync();

			var message = task.Result;

			if (message.IsSuccessStatusCode == false)
			{
				throw new Exception($"{message.StatusCode}: {message.ReasonPhrase}");
			}

			var content = message.Content.ReadAsAsync<T>();

			return content.Result;
		}

		public T Get<T>(Uri uri)
		{
			return Get<T>(uri.ToString());
		}

		public HttpStatusCode Put<T>(string url, ChangeSet<T> changes)
		{
			var task = TestServer.CreateRequest(url)
				.PutAsync(changes);

			return task.Result.StatusCode;
		}

		public HttpStatusCode Put<T>(Uri uri, ChangeSet<T> changes)
		{
			return Put<T>(uri.ToString(), changes);
		}

		public HttpStatusCode Post<T>(string url, T item)
		{
			var task = TestServer.CreateRequest(url)
				.PostAsync(item);

			return task.Result.StatusCode;
		}

		public HttpStatusCode Post<T>(Uri uri, T item)
		{
			return Post<T>(uri.ToString(), item);
		}

		public HttpStatusCode Delete(string url)
		{
			var task = TestServer.CreateRequest(url)
				.DeleteAsync();

			return task.Result.StatusCode;
		}

		public HttpStatusCode Delete(Uri uri)
		{
			return Delete(uri.ToString());
		}
	}
}
