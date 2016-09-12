using System.Web.Http;

using Owin;

using StructureMap;

namespace TestWebApiService
{
	public class Startup
	{
		public void Configuration(IAppBuilder appBuilder)
		{
			HttpConfiguration config = new HttpConfiguration();

			config.DependencyResolver = new StructureMapDependencyResolver(new Container(new TestWebApiServiceRegistry()));

			config.MapHttpAttributeRoutes();

			appBuilder.UseWebApi(config);
		}
	}
}
