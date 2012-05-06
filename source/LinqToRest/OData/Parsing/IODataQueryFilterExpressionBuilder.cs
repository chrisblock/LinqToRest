using System.Linq.Expressions;

using LinqToRest.OData.Filters;

namespace LinqToRest.OData.Parsing
{
	public interface IODataQueryFilterExpressionBuilder
	{
		LambdaExpression BuildExpression(ODataQueryFilterExpression query);
	}
}
