using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http.Filters;

namespace TestWebApiService.ActionFilters
{
	public class ODataQueryAttribute : ActionFilterAttribute
	{
		public ODataQueryAttribute()
		{
			
		}

		public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
		{
			base.OnActionExecuted(actionExecutedContext);

			if (actionExecutedContext.Result != null)
			{
				var responseType = actionExecutedContext.Result.GetType().GetGenericArguments().Single();

				var queryString = actionExecutedContext.Request.RequestUri.Query;

				if (typeof (IQueryable).IsAssignableFrom(responseType) && (String.IsNullOrWhiteSpace(queryString) == false))
				{
					var matches = Regex.Matches(queryString, @"[?&]([^=]+)=([^&]+)").Cast<Match>().ToList();

					foreach (var match in matches)
					{
						var groups = match.Groups.Cast<Group>().Skip(1).ToList();
						var type = groups[0].Value;
						var expr = groups[1].Value;

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
}
