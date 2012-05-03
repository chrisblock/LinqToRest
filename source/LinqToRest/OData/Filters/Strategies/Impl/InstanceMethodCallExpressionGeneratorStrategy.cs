using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LinqToRest.OData.Filters.Strategies.Impl
{
	public class InstanceMethodCallExpressionGeneratorStrategy : IMethodCallExpressionGeneratorStrategy
	{
		public Expression Generate(Function method, IEnumerable<Expression> arguments)
		{
			var instance = arguments.First();
			var args = arguments.Skip(1);

			var methodName = method.GetDotNetMethodName();

			return Expression.Call(instance, methodName, new Type[0], args.ToArray());
		}
	}
}
