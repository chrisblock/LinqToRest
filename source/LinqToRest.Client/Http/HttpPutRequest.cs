using System;
using System.Net.Http;
using System.Threading.Tasks;

using Changes;

namespace LinqToRest.Client.Http
{
	public class HttpPutRequest<T> : HttpRequest
	{
		private readonly Uri _uri;
		private readonly ChangeSet<T> _data;

		public HttpPutRequest(Uri uri, ChangeSet<T> data)
		{
			_uri = uri;
			_data = data;
		}

		protected override Task<HttpResponseMessage> PerformRequest()
		{
			Task<HttpResponseMessage> result;

			using (var httpClient = new HttpClient())
			{
				result = httpClient.PutAsync(_uri, new ObjectContent(_data.GetType(), _data, null));
			}

			return result;
		}
	}
}
