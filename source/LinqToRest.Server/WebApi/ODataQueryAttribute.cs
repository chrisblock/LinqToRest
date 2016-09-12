using System;
using System.Linq;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

using LinqToRest.Server.OData;
using LinqToRest.Server.OData.Expressions;
using LinqToRest.Server.OData.Expressions.Impl;
using LinqToRest.Server.OData.Parsing;
using LinqToRest.Server.OData.Parsing.Impl;

namespace LinqToRest.Server.WebApi
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
	public class ODataQueryAttribute : ActionFilterAttribute
	{
		public override void OnActionExecuting(HttpActionContext actionContext)
		{
			base.OnActionExecuting(actionContext);
		}

		public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
		{
			if (actionExecutedContext == null)
			{
				throw new ArgumentNullException(nameof (actionExecutedContext));
			}

			if (actionExecutedContext.Request == null)
			{
				throw new ArgumentException("The HttpActionExecutedContext cannot contain a null HttpRequestMessage.");
			}

			var request = actionExecutedContext.Request;
			var response = actionExecutedContext.Response;

			IQueryable query;
			if ((response != null) && response.TryGetContentValue(out query))
			{
				if ((request.RequestUri != null) && (String.IsNullOrWhiteSpace(request.RequestUri.Query) == false))
				{
					var itemType = query.ElementType;

					var requestUri = request.RequestUri;

					var parser = new ODataUriParser(new ODataQueryParser(new ODataQueryPartParserStrategy()), new ExpressionODataQueryVisitor(new FilterExpressionBuilder()));

					var expression = parser.Parse(itemType, requestUri);

					var fn = expression.Compile();

					var result = fn.DynamicInvoke(query);

					var oldContent = (ObjectContent) response.Content;

					var newContent = new ObjectContent(expression.ReturnType, result, oldContent.Formatter, oldContent.Headers.ContentType.MediaType);

					response.Content = newContent;
				}
			}
		}
	}
}
