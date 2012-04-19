using System.Net;

namespace LinqToRest.Http.Impl
{
	public class HttpRequestProcessor : IHttpRequestProcessor
	{
		public HttpWebResponse GetRequestResult(HttpWebRequest request)
		{
			var response = (HttpWebResponse)request.GetResponse();

			return response;
		}
	}
}
