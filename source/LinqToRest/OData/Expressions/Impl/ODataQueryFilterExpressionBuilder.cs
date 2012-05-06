using System;
using System.Linq.Expressions;

using LinqToRest.OData.Filters;

namespace LinqToRest.OData.Expressions.Impl
{
	public class ODataQueryFilterExpressionBuilder : IODataQueryFilterExpressionBuilder
	{
		public LambdaExpression BuildExpression<T>(ODataQueryFilterExpression filter)
		{
			return BuildExpression(typeof (T), filter);
		}

		public LambdaExpression BuildExpression(Type type, ODataQueryFilterExpression filter)
		{
			var parameter = ExpressionHelper.CreateParameter(type);

			var filterExpressionTranslator = new ODataQueryFilterExpressionTranslator(parameter);

			var body = filterExpressionTranslator.Translate(filter);

			return Expression.Lambda(body, false, parameter);
		}
	}
}
