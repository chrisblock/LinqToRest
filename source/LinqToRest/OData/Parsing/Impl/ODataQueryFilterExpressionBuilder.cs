using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

using LinqToRest.OData.Building.Strategies.Impl;
using LinqToRest.OData.Filters;

namespace LinqToRest.OData.Parsing.Impl
{
	public class ODataQueryFilterExpressionBuilder : IODataQueryFilterExpressionBuilder
	{
		private readonly Type _type;

		public ODataQueryFilterExpressionBuilder(Type type)
		{
			_type = type;
		}

		public LambdaExpression BuildExpression(ODataQueryFilterExpression expression)
		{
			var parameter = Expression.Parameter(_type, "p");

			var filterExpressionTranslator = new ODataQueryFilterExpressionTranslator(parameter);

			var body = filterExpressionTranslator.Translate(expression);

			return Expression.Lambda(body, false, parameter);
		}
	}
}
