using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;

using Microsoft.Owin.Testing;

namespace LinqToRest.IntegrationTests
{
	public static class RequestBuilderExtensions
	{
		public static JsonMediaTypeFormatter DefaultJsonMediaTypeFormatter { get; } = new JsonMediaTypeFormatter();

		public static async Task<HttpResponseMessage> DeleteAsync(this RequestBuilder builder)
		{
			return await builder
				.SendAsync("DELETE");
		}

		public static async Task<HttpResponseMessage> PostAsync<TModel>(this RequestBuilder builder, TModel model)
		{
			return await builder
				.And(request => request.Content = new ObjectContent<TModel>(model, DefaultJsonMediaTypeFormatter))
				.PostAsync();
		}

		public static async Task<HttpResponseMessage> PutAsync<TModel>(this RequestBuilder builder, TModel model)
		{
			return await builder
				.And(request => request.Content = new ObjectContent<TModel>(model, DefaultJsonMediaTypeFormatter))
				.SendAsync("PUT");
		}
	}
}
