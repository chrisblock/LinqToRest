using System.Linq.Expressions;

using LinqToRest.OData.Filters;

namespace LinqToRest.Client.OData
{
	public interface IFilterExpressionTranslator
	{
		FilterExpression Translate(Expression expression);
	}
}
