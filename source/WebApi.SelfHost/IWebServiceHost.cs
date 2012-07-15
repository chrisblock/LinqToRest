using System;

namespace WebApi.SelfHost
{
	public interface IWebServiceHost : IDisposable
	{
		void Start(RouteConfigurationTable routeConfigurationTable);
	}
}
