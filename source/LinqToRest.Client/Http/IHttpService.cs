using System;
using System.Net.Http;

namespace LinqToRest.Client.Http
{
	public interface IHttpService
	{
		string Get(string url);
		string Get(Uri uri);

		void Put(string url, HttpContent content);
		void Put(Uri uri, HttpContent content);

		void Post(string url, HttpContent content);
		void Post(Uri uri, HttpContent content);

		void Delete(string url);
		void Delete(Uri uri);
	}
}
