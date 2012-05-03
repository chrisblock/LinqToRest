using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace LinqToRest.OData.Filters.Strategies.Impl
{
	public class CastMethodCallExpressionGeneratorStrategy : IMethodCallExpressionGeneratorStrategy
	{
		public Expression Generate(Function method, IEnumerable<Expression> arguments)
		{
			throw new NotImplementedException();
		}
	}
}
