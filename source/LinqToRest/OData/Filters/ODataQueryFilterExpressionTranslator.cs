using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

using LinqToRest.OData.Filters.Strategies.Impl;

using Remotion.Linq;

namespace LinqToRest.OData.Filters
{
	public class ODataQueryFilterExpressionTranslator
	{
		private static readonly TypeComparer TypeComparer = new TypeComparer();

		private static readonly MethodInfo ChangeType = ReflectionUtility.GetMethod(() => Convert.ChangeType(null, typeof (int)));

		private readonly ParameterExpression _parameter;

		public ODataQueryFilterExpressionTranslator(ParameterExpression parameter)
		{
			_parameter = parameter;
		}

		public virtual Expression Translate(ODataQueryFilterExpression filter)
		{
			Expression result;

			switch (filter.ExpressionType)
			{
				case ODataQueryFilterExpressionType.Constant:
					result = TranslateConstant((ODataQueryConstantFilterExpression) filter);
					break;
				case ODataQueryFilterExpressionType.Unary:
					result = TranslateUnary((ODataQueryUnaryFilterExpression)filter);
					break;
				case ODataQueryFilterExpressionType.Binary:
					result = TranslateBinary((ODataQueryBinaryFilterExpression)filter);
					break;
				case ODataQueryFilterExpressionType.MethodCall:
					result = TranslateMethodCall((ODataQueryMethodCallFilterExpression)filter);
					break;
				case ODataQueryFilterExpressionType.MemberAccess:
					result = TranslateMemberAccess((ODataQueryMemberAccessFilterExpression)filter);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			return result;
		}

		protected virtual Expression TranslateConstant(ODataQueryConstantFilterExpression constant)
		{
			return Expression.Constant(constant.Value, constant.Type);
		}

		protected virtual Expression TranslateUnary(ODataQueryUnaryFilterExpression unary)
		{
			var operand = Translate(unary.Operand);

			var expressionType = unary.Operator.GetDotNetExpressionType();

			return Expression.MakeUnary(expressionType, operand, null);
		}

		protected virtual Expression TranslateBinary(ODataQueryBinaryFilterExpression binary)
		{
			var left = Translate(binary.Left);
			var right = Translate(binary.Right);

			if (left.Type != right.Type)
			{
				CoerceTypes(ref left, ref right);
			}

			var expressionType = binary.Operator.GetDotNetExpressionType();

			return Expression.MakeBinary(expressionType, left, right);
		}

		private static void CoerceTypes(ref Expression left, ref Expression right)
		{
			var leftType = left.Type;
			var rightType = right.Type;

			var targetType = (TypeComparer.Compare(leftType, rightType) < 0)
				? rightType
				: leftType;

			if (leftType != targetType)
			{
				left = Expression.Convert(left, targetType);
			}

			if (rightType != targetType)
			{
				right = Expression.Convert(right, targetType);
			}
		}

		protected virtual Expression TranslateMethodCall(ODataQueryMethodCallFilterExpression methodCall)
		{
			var arguments = methodCall.Arguments.Select(Translate).ToArray();

			var strategy = new MethodCallExpressionGeneratorStrategy();

			return strategy.Generate(methodCall.Method, arguments);
		}

		protected virtual Expression TranslateMemberAccess(ODataQueryMemberAccessFilterExpression memberAccess)
		{
			Expression instance = _parameter;

			if (memberAccess.Instance != null)
			{
				instance = Translate(memberAccess.Instance);
			}

			var members = instance.Type.GetMember(memberAccess.Member).ToList();

			if (members.Count != 1)
			{
				throw new ArgumentException(String.Format("Could not find member '{0}' on type '{1}'.", memberAccess.Member, instance.Type));
			}

			var memberInfo = members.Single();

			return Expression.MakeMemberAccess(instance, memberInfo);
		}
	}
}
