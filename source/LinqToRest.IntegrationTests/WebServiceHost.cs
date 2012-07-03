using System;
using System.Web.Http;
using System.Web.Http.SelfHost;

namespace LinqToRest.IntegrationTests
{
	public class WebServiceHost : IDisposable
	{
		private const string BaseAddress = @"http://localhost:6789";
		private readonly HttpSelfHostServer _server;

		public WebServiceHost()
		{
			var config = new HttpSelfHostConfiguration(BaseAddress);

			TestWebApiService.WebApiApplication.RegisterRoutes((name, routeTemplate, defaults) => config.Routes.MapHttpRoute(name, routeTemplate, defaults));

			_server = new HttpSelfHostServer(config);

			_server.OpenAsync().Wait();
		}

		public void Dispose()
		{
			_server.CloseAsync().Wait();
		}
	}
}
