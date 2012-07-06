using System;
using System.Net;

using Changes;

namespace LinqToRest.Client.Http
{
	public interface IHttpService
	{
		T Get<T>(string url);
		T Get<T>(Uri uri);

		HttpStatusCode Put<T>(string url, ChangeSet<T> changes);
		HttpStatusCode Put<T>(Uri uri, ChangeSet<T> changes);

		HttpStatusCode Post<T>(string url, T item);
		HttpStatusCode Post<T>(Uri uri, T item);

		HttpStatusCode Delete(string url);
		HttpStatusCode Delete(Uri uri);
	}
}
