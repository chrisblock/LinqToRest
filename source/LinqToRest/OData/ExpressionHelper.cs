using System;
using System.Linq.Expressions;

namespace LinqToRest.OData
{
	public static class ExpressionHelper
	{
		private static int _parameterCount = 0;

		public static LambdaExpression Lambda<T>(string memberName)
		{
			var type = typeof (T);

			return Lambda(type, memberName);
		}

		public static LambdaExpression Lambda(Type type, string memberName)
		{
			var property = type.GetProperty(memberName);

			var parameter = Expression.Parameter(type, String.Format("@{0}_{1}_", type.Name.ToLowerInvariant(), ++_parameterCount));

			var body = Expression.MakeMemberAccess(parameter, property);

			return Expression.Lambda(body, parameter);
		}
	}
}
