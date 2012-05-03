using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LinqToRest.OData.Filters.Strategies.Impl
{
	public class PropertyExpressionGeneratorStrategy : IMethodCallExpressionGeneratorStrategy
	{
		public Expression Generate(Function method, IEnumerable<Expression> arguments)
		{
			var instance = arguments.First();

			var propertyName = method.GetDotNetMethodName();

			var propertyInfo = instance.Type.GetProperty(propertyName);

			if (propertyInfo == null)
			{
				throw new NotSupportedException(String.Format("Method '{0}' not supported for type '{1}'.", method.GetODataQueryMethodName(), instance.Type));
			}

			return Expression.MakeMemberAccess(instance, propertyInfo);
		}
	}
}
