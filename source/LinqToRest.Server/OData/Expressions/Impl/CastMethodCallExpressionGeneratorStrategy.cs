using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using LinqToRest.OData.Filters;

namespace LinqToRest.Server.OData.Expressions.Impl
{
	public class CastMethodCallExpressionGeneratorStrategy : IMethodCallExpressionGeneratorStrategy
	{
		public Expression Generate(Function method, IEnumerable<Expression> arguments)
		{
			throw new NotImplementedException();
		}
	}
}
