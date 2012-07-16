using System;
using System.Linq;
using System.Linq.Expressions;

using LinqToRest.OData.Filters;
using LinqToRest.Server.OData.Expressions.Impl;

namespace LinqToRest.Server.OData.Expressions
{
	public class FilterExpressionTranslator
	{
		private static readonly BestFitTypeDeterminer Determiner = new BestFitTypeDeterminer();

		private readonly ParameterExpression _parameter;

		public FilterExpressionTranslator(ParameterExpression parameter)
		{
			_parameter = parameter;
		}

		public virtual Expression Translate(FilterExpression filter)
		{
			Expression result;

			switch (filter.ExpressionType)
			{
				case FilterExpressionType.Constant:
					result = TranslateConstant((ConstantFilterExpression) filter);
					break;
				case FilterExpressionType.Unary:
					result = TranslateUnary((UnaryFilterExpression)filter);
					break;
				case FilterExpressionType.Binary:
					result = TranslateBinary((BinaryFilterExpression)filter);
					break;
				case FilterExpressionType.MethodCall:
					result = TranslateMethodCall((MethodCallFilterExpression)filter);
					break;
				case FilterExpressionType.MemberAccess:
					result = TranslateMemberAccess((MemberAccessFilterExpression)filter);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			return result;
		}

		protected virtual Expression TranslateConstant(ConstantFilterExpression constant)
		{
			return Expression.Constant(constant.Value, constant.Type);
		}

		protected virtual Expression TranslateUnary(UnaryFilterExpression unary)
		{
			var operand = Translate(unary.Operand);

			var expressionType = unary.Operator.GetDotNetExpressionType();

			return Expression.MakeUnary(expressionType, operand, null);
		}

		protected virtual Expression TranslateBinary(BinaryFilterExpression binary)
		{
			var left = Translate(binary.Left);
			var right = Translate(binary.Right);

			if (left.Type != right.Type)
			{
				if (left.Type == typeof (object))
				{
					left = Expression.Convert(left, right.Type);
				}
				else if (right.Type == typeof (object))
				{
					right = Expression.Convert(right, left.Type);
				}
				else if (left.Type.IsAssignableFrom(right.Type))
				{
					right = Expression.Convert(right, left.Type);
				}
				else if (right.Type.IsAssignableFrom(left.Type))
				{
					left = Expression.Convert(left, right.Type);
				}
				// TODO: do we even need these branches?
				//else if ((left.NodeType == ExpressionType.Constant) && (right.NodeType != ExpressionType.Constant))
				//{
				//    left = Expression.Convert(left, right.Type);
				//}
				//else if ((left.NodeType != ExpressionType.Constant) && (right.NodeType == ExpressionType.Constant))
				//{
				//    right = Expression.Convert(right, left.Type);
				//}
				else
				{
					CoerceTypes(ref left, ref right);
				}
			}

			var expressionType = binary.Operator.GetDotNetExpressionType();

			return Expression.MakeBinary(expressionType, left, right);
		}

		private static void CoerceTypes(ref Expression left, ref Expression right)
		{
			var leftType = left.Type;
			var rightType = right.Type;

			var targetType = Determiner.DetermineBestFit(leftType, rightType);

			if (leftType != targetType)
			{
				left = Expression.Convert(left, targetType);
			}

			if (rightType != targetType)
			{
				right = Expression.Convert(right, targetType);
			}
		}

		protected virtual Expression TranslateMethodCall(MethodCallFilterExpression methodCall)
		{
			var arguments = methodCall.Arguments.Select(Translate).ToArray();

			var strategy = new MethodCallExpressionGeneratorStrategy();

			return strategy.Generate(methodCall.Method, arguments);
		}

		protected virtual Expression TranslateMemberAccess(MemberAccessFilterExpression memberAccess)
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
