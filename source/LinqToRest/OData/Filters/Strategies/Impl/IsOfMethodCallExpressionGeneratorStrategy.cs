using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LinqToRest.OData.Filters.Strategies.Impl
{
	public class IsOfMethodCallExpressionGeneratorStrategy : IMethodCallExpressionGeneratorStrategy
	{
		public Expression Generate(Function method, IEnumerable<Expression> arguments)
		{
			var instance = arguments.First();
			var type = (ConstantExpression)arguments.Skip(1).Last();

			return Expression.TypeIs(instance, (Type)type.Value);
		}
	}
}
