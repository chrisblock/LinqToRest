using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace LinqToRest.Client.Http
{
	public class HttpDeleteRequest : HttpRequest
	{
		private readonly Uri _uri;

		public HttpDeleteRequest(Uri uri)
		{
			_uri = uri;
		}

		protected override Task<HttpResponseMessage> PerformRequest()
		{
			Task<HttpResponseMessage> result;

			using (var httpClient = new HttpClient())
			{
				result = httpClient.DeleteAsync(_uri);
			}

			return result;
		}
	}
}
