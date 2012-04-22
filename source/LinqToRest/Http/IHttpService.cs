using System;

namespace LinqToRest.Http
{
	public interface IHttpService
	{
		string Get(string url);
		string Get(Uri uri);
	}
}
