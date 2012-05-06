using System;

namespace LinqToRest.OData.Filters
{
	public abstract class ODataQueryFilterExpression
	{
		public abstract ODataQueryFilterExpressionType ExpressionType { get; }

		public static ODataQueryBinaryFilterExpression Binary(ODataQueryFilterExpression left, ODataQueryFilterExpressionOperator op, ODataQueryFilterExpression right)
		{
			return new ODataQueryBinaryFilterExpression(left, op, right);
		}

		public static ODataQueryConstantFilterExpression Constant<T>(T value)
		{
			return new ODataQueryConstantFilterExpression(value, typeof(T));
		}

		public static ODataQueryConstantFilterExpression Constant(object value, Type type)
		{
			return new ODataQueryConstantFilterExpression(value, type);
		}

		public static ODataQueryMethodCallFilterExpression MethodCall(Function method, params ODataQueryFilterExpression[] arguments)
		{
			return new ODataQueryMethodCallFilterExpression(method, arguments);
		}

		public static ODataQueryUnaryFilterExpression Unary(ODataQueryFilterExpressionOperator op, ODataQueryFilterExpression operand)
		{
			return new ODataQueryUnaryFilterExpression(op, operand);
		}

		public static ODataQueryMemberAccessFilterExpression MemberAccess(string memberName)
		{
			return new ODataQueryMemberAccessFilterExpression(null, memberName);
		}

		public static ODataQueryMemberAccessFilterExpression MemberAccess(ODataQueryFilterExpression instance, string memberName)
		{
			return new ODataQueryMemberAccessFilterExpression(instance, memberName);
		}
	}
}
