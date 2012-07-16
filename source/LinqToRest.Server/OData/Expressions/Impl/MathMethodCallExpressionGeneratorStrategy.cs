using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using LinqToRest.OData.Filters;

namespace LinqToRest.Server.OData.Expressions.Impl
{
	public class MathMethodCallExpressionGeneratorStrategy : IMethodCallExpressionGeneratorStrategy
	{
		public Expression Generate(Function method, IEnumerable<Expression> arguments)
		{
			var methodName = method.GetDotNetMethodName();

			return Expression.Call(typeof (Math), methodName, new Type[0], arguments.ToArray());
		}
	}
}
