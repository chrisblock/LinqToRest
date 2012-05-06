using System;
using System.Linq.Expressions;

namespace LinqToRest.OData
{
	public interface IODataQueryTranslator
	{
		LambdaExpression Translate<T>(ODataQuery query);
		LambdaExpression Translate(Type type, ODataQuery query);
	}
}
