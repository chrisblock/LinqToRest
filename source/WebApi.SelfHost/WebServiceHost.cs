using System;
using System.Web.Http.SelfHost;

namespace WebApi.SelfHost
{
	public class WebServiceHost : MarshalByRefObject, IWebServiceHost
	{
		private const string BaseAddress = @"http://localhost:6789";
		private HttpSelfHostServer _server;

		public void Start(RouteConfigurationTable routeConfigurationTable)
		{
			if (_server == null)
			{
				var config = new HttpSelfHostConfiguration(BaseAddress);

				routeConfigurationTable.Configure(config.Routes);

				_server = new HttpSelfHostServer(config);

				var openTask = _server.OpenAsync();

				openTask.Wait();

				if (openTask.IsFaulted || (openTask.Exception != null))
				{
					throw new ApplicationException("Unable to open web service host.");
				}
			}
		}

		public void Dispose()
		{
			if (_server != null)
			{
				var closeTask = _server.CloseAsync();

				closeTask.Wait();

				if (closeTask.IsFaulted || (closeTask.Exception != null))
				{
					throw new ApplicationException("Unable to close web service host.");
				}
			}
		}
	}
}
