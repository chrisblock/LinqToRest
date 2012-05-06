using System;
using System.Linq.Expressions;

using LinqToRest.OData.Filters;

namespace LinqToRest.OData.Expressions
{
	public interface IODataQueryFilterExpressionBuilder
	{
		LambdaExpression BuildExpression<T>(ODataQueryFilterExpression filter);
		LambdaExpression BuildExpression(Type type, ODataQueryFilterExpression filter);
	}
}
