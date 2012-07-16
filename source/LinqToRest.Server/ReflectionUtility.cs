using System;
using System.Linq.Expressions;
using System.Reflection;

namespace LinqToRest.Server
{
	public static class ReflectionUtility
	{
		public static MethodInfo GetMethod<T>(Expression<Func<T>> methodCall)
		{
			var methodCallExpression = methodCall.Body as MethodCallExpression;

			if (methodCallExpression == null)
			{
				throw new ArgumentException(String.Format("'{0}' is not a MethodCallExpression.", methodCall.Body));
			}

			var methodInfo = methodCallExpression.Method;

			return methodInfo;
		}
	}
}
