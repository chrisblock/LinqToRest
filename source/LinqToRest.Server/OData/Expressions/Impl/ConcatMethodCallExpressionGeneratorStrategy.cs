using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

using LinqToRest.OData.Filters;

namespace LinqToRest.Server.OData.Expressions.Impl
{
	public class ConcatMethodCallExpressionGeneratorStrategy : IMethodCallExpressionGeneratorStrategy
	{
		private static readonly MethodInfo ConcatMethodInfo = ReflectionUtility.GetMethod(() => String.Concat(String.Empty, String.Empty));

		public Expression Generate(Function method, IEnumerable<Expression> arguments)
		{
			return Expression.Call(ConcatMethodInfo, arguments);
		}
	}
}
