using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace LinqToRest
{
	internal static class MemberInfoExtensions
	{
		public static MemberInfo GetMemberInfo<T, TProperty>(this T item, Expression<Func<T, TProperty>> expression)
		{
			var memberExpression = expression.Body as MemberExpression;

			if (memberExpression == null)
			{
				throw new ArgumentException(String.Format("'{0}' is not a member expression.", expression.Body));
			}

			return memberExpression.Member;
		}

		public static IEnumerable<T> GetCustomAttributes<T>(this MemberInfo memberInfo, bool inherit = false) where T : Attribute
		{
			return memberInfo.GetCustomAttributes(typeof (T), inherit).Cast<T>();
		}

		public static string GetServiceUrl(this MemberInfo memberInfo)
		{
			var serviceUrlAttributes = memberInfo.GetCustomAttributes<ServiceUrlAttribute>().ToList();

			if (serviceUrlAttributes.Count != 1)
			{
				throw new ArgumentException(String.Format("ServiceUrlAttribute not found on '{0}'.", memberInfo.Name));
			}

			var url = serviceUrlAttributes.Single().Url;

			return url;
		}

		public static Uri GetServiceUri(this MemberInfo memberInfo)
		{
			var url = memberInfo.GetServiceUrl();

			return new Uri(url);
		}
	}
}
