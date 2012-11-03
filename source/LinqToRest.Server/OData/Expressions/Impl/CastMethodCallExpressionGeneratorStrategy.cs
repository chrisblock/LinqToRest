using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using LinqToRest.OData.Filters;

namespace LinqToRest.Server.OData.Expressions.Impl
{
	public class CastMethodCallExpressionGeneratorStrategy : IMethodCallExpressionGeneratorStrategy
	{
		public Expression Generate(Function method, IEnumerable<Expression> arguments)
		{
			var args = arguments.ToList();

			var expression = args.First();

			var typeExpression = args.ElementAt(1);

			var constantTypeExpression = typeExpression as ConstantExpression;

			if (constantTypeExpression == null)
			{
				throw new ArgumentException(String.Format("'{0}' is not a ConstantExpression.", typeExpression));
			}

			var type = constantTypeExpression.Value as Type;

			if (type == null)
			{
				throw new ArgumentException(String.Format("'{0}' is not a Type.", constantTypeExpression.Value));
			}

			return Expression.Convert(expression, type);
		}
	}
}
