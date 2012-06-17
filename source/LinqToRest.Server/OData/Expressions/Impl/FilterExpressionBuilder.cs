using System;
using System.Linq.Expressions;

using LinqToRest.OData.Filters;

namespace LinqToRest.Server.OData.Expressions.Impl
{
	public class FilterExpressionBuilder : IFilterExpressionBuilder
	{
		public LambdaExpression BuildExpression<T>(FilterExpression filter)
		{
			return BuildExpression(typeof (T), filter);
		}

		public LambdaExpression BuildExpression(Type type, FilterExpression filter)
		{
			var parameter = ExpressionHelper.CreateParameter(type);

			var filterExpressionTranslator = new FilterExpressionTranslator(parameter);

			var body = filterExpressionTranslator.Translate(filter);

			return Expression.Lambda(body, false, parameter);
		}
	}
}
