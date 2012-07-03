using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace LinqToRest.Client.Http
{
	public class HttpGetRequest : HttpRequest
	{
		private readonly Uri _uri;

		public HttpGetRequest(Uri uri)
		{
			_uri = uri;
		}

		protected override Task<HttpResponseMessage> PerformRequest()
		{
			Task<HttpResponseMessage> result;

			using (var httpClient = new HttpClient())
			{
				result = httpClient.GetAsync(_uri);
			}

			return result;
		}
	}
}
