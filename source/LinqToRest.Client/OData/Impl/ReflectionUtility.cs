using System;
using System.Linq.Expressions;
using System.Reflection;

namespace LinqToRest.Client.OData.Impl
{
	internal static class ReflectionUtility
	{
		internal static MethodInfo GetMethod<T>(Expression<Func<T>> expression)
		{
			var call = expression.Body as MethodCallExpression;

			if (call == null)
			{
				throw new ArgumentException($"'{expression.Body}' is not a MethodCallExpression.", nameof (expression));
			}

			return call.Method;
		}
	}
}
