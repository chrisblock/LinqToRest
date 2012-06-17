using System;
using System.Linq.Expressions;

using LinqToRest.OData.Filters;

namespace LinqToRest.Server.OData.Expressions
{
	public interface IFilterExpressionBuilder
	{
		LambdaExpression BuildExpression<T>(FilterExpression filter);
		LambdaExpression BuildExpression(Type type, FilterExpression filter);
	}
}
