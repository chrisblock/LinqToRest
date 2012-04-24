using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;

namespace TestWebApiService.ActionFilters
{
	public class ODataQueryAttribute : ActionFilterAttribute
	{
		public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
		{
			base.OnActionExecuted(actionExecutedContext);

			var responseType = actionExecutedContext.Result.GetType().GetGenericArguments().Single();

			var queryString = actionExecutedContext.Request.RequestUri.Query;

			if (typeof(IQueryable).IsAssignableFrom(responseType) && (String.IsNullOrWhiteSpace(queryString) == false))
			{
				queryString = queryString.Substring(1);

				var queryStringPart = queryString.Split('&');

				foreach (var part in queryStringPart)
				{
					var arr = part.Split('=');

					var type = arr[0];
					var expr = arr[1];

					if (type.Equals("$format", StringComparison.OrdinalIgnoreCase))
					{

					}
					else if (type.Equals("$format", StringComparison.OrdinalIgnoreCase))
					{

					}
				}
			}
		}
	}
}
