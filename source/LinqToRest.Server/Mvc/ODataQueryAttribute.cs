using System;
using System.Linq;
using System.Web.Mvc;

using LinqToRest.Server.OData;

namespace LinqToRest.Server.Mvc
{
	public class ODataQueryAttribute : ActionFilterAttribute
	{
		public override void OnResultExecuting(ResultExecutingContext resultExecutingContext)
		{
			base.OnResultExecuting(resultExecutingContext);

			if (resultExecutingContext == null)
			{
				throw new ArgumentNullException("resultExecutingContext", "Cannot apply OData Query with a null execution context.");
			}

			var httpContext = resultExecutingContext.HttpContext;

			if (httpContext == null)
			{
				throw new ArgumentException("The HttpContext was null. Could not process OData Query request.");
			}

			var request = httpContext.Request;
			var response = httpContext.Response;

			var result = resultExecutingContext.Result;

			if (result != null)
			{
				var resultType = result.GetType();
			}
		}

		public override void OnResultExecuted(ResultExecutedContext resultExecutedContext)
		{
			base.OnResultExecuted(resultExecutedContext);

			if (resultExecutedContext == null)
			{
				throw new ArgumentNullException("resultExecutedContext", "Cannot apply OData Query with a null execution context.");
			}

			if (resultExecutedContext.HttpContext == null)
			{
				throw new ArgumentException("The HttpContext was null. Could not process OData Query request.");
			}

			var response = resultExecutedContext.HttpContext.Response;
			var request = resultExecutedContext.HttpContext.Request;

			var result = resultExecutedContext.Result;

			if (result != null)
			{
				var resultType = result.GetType();

				var responseType = resultType.GetGenericArguments().Single();

				var url = request.RawUrl;

				var uri = new Uri(url);

				var queryString = uri.Query;

				if (typeof(IQueryable).IsAssignableFrom(responseType) && (String.IsNullOrWhiteSpace(queryString) == false))
				{
					var itemType = responseType.GetGenericArguments().Single();

					var parser = new ODataUriParser();

					var expression = parser.Parse(itemType, uri);

					var fn = expression.Compile();

					// TODO: need to pass in current result here
					var data = fn.DynamicInvoke();

					var actionResult = Activator.CreateInstance(resultType) as ActionResult;

					resultExecutedContext.Result = actionResult;
				}
			}
		}
	}
}
