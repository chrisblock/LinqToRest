using System;

namespace WebApi.SelfHost
{
	public class WebServiceHostProxy : IWebServiceHost
	{
		private readonly AppDomain _webServiceHostDomain;
		private readonly IWebServiceHost _webServiceHost;

		public WebServiceHostProxy(AppDomain webServiceHostDomain)
		{
			_webServiceHostDomain = webServiceHostDomain;
			_webServiceHost = _webServiceHostDomain.CreateInstanceAndUnwrap<WebServiceHost>();
		}

		public void Start(RouteConfigurationTable routeConfigurationTable)
		{
			_webServiceHost.Start(routeConfigurationTable);
		}

		public void Dispose()
		{
			_webServiceHost.Dispose();

			AppDomain.Unload(_webServiceHostDomain);
		}
	}
}
