using System;
using System.Linq.Expressions;

using LinqToRest.OData;

namespace LinqToRest.Server.OData
{
	public interface IODataQueryTranslator
	{
		LambdaExpression Translate<T>(ODataQuery query);
		LambdaExpression Translate(Type type, ODataQuery query);
	}
}
