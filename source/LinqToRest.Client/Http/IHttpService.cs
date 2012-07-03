using System;
using System.Net;
using System.Net.Http;

namespace LinqToRest.Client.Http
{
	public interface IHttpService
	{
		T Get<T>(string url);
		T Get<T>(Uri uri);

		HttpStatusCode Put(string url, HttpContent content);
		HttpStatusCode Put(Uri uri, HttpContent content);

		HttpStatusCode Post(string url, HttpContent content);
		HttpStatusCode Post(Uri uri, HttpContent content);

		HttpStatusCode Delete(string url);
		HttpStatusCode Delete(Uri uri);
	}
}
