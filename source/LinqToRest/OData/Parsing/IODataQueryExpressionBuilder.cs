using System.Linq.Expressions;

namespace LinqToRest.OData.Parsing
{
	public interface IODataQueryExpressionBuilder
	{
		LambdaExpression BuildExpression(string query);
	}
}
