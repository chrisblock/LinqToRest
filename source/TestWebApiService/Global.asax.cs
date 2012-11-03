using System.Web;
using System.Web.Mvc;

namespace TestWebApiService
{
	public class TestWebApiServiceApplication : HttpApplication
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
		}
	}
}
