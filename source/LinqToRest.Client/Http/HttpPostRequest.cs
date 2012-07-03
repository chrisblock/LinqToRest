using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace LinqToRest.Client.Http
{
	public class HttpPostRequest<T> : HttpRequest
	{
		private readonly Uri _uri;
		private readonly T _data;

		public HttpPostRequest(Uri uri, T data)
		{
			_uri = uri;
			_data = data;
		}

		protected override Task<HttpResponseMessage> PerformRequest()
		{
			Task<HttpResponseMessage> result;

			using (var httpClient = new HttpClient())
			{
				result = httpClient.PostAsync(_uri, new ObjectContent(_data.GetType(), _data, null));
			}

			return result;
		}
	}
}
