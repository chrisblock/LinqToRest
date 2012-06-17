using System;
using System.Linq.Expressions;

namespace LinqToRest.Server.OData.Expressions
{
	public static class ExpressionHelper
	{
		private static int _parameterCount = 0;
		private static readonly object ParameterLock = new object();

		public static ParameterExpression CreateParameter<T>()
		{
			return CreateParameter(typeof (T));
		}

		public static ParameterExpression CreateParameter(Type type)
		{
			string parameterName;

			lock (ParameterLock)
			{
				parameterName = String.Format("@_{0}_{1}_", type.Name, _parameterCount);
				_parameterCount++;
			}

			return Expression.Parameter(type, parameterName);
		}

		public static LambdaExpression Lambda<T>(string memberName)
		{
			var type = typeof (T);

			return Lambda(type, memberName);
		}

		public static LambdaExpression Lambda(Type type, string memberName)
		{
			var property = type.GetProperty(memberName);

			var parameter = CreateParameter(type);

			var body = Expression.MakeMemberAccess(parameter, property);

			return Expression.Lambda(body, parameter);
		}
	}
}
