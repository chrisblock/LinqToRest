using System;

namespace LinqToRest.OData.Filters
{
	public abstract class FilterExpression
	{
		public abstract FilterExpressionType ExpressionType { get; }

		public static BinaryFilterExpression Binary(FilterExpression left, FilterExpressionOperator op, FilterExpression right)
		{
			return new BinaryFilterExpression(left, op, right);
		}

		public static ConstantFilterExpression Constant<T>(T value)
		{
			return new ConstantFilterExpression(value, typeof(T));
		}

		public static ConstantFilterExpression Constant(object value, Type type)
		{
			return new ConstantFilterExpression(value, type);
		}

		public static MethodCallFilterExpression MethodCall(Function method, params FilterExpression[] arguments)
		{
			return new MethodCallFilterExpression(method, arguments);
		}

		public static UnaryFilterExpression Unary(FilterExpressionOperator op, FilterExpression operand)
		{
			return new UnaryFilterExpression(op, operand);
		}

		public static MemberAccessFilterExpression MemberAccess(string memberName)
		{
			return new MemberAccessFilterExpression(null, memberName);
		}

		public static MemberAccessFilterExpression MemberAccess(FilterExpression instance, string memberName)
		{
			return new MemberAccessFilterExpression(instance, memberName);
		}
	}
}
