using System.Net;

namespace LinqToRest.Http
{
	public interface IHttpRequestProcessor
	{
		HttpWebResponse GetRequestResult(HttpWebRequest request);
	}
}
