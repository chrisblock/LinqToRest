using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

using LinqToRest.OpenData.Strategies;

namespace LinqToRest.OpenData
{
	public class ODataExpressionVisitor : ExpressionVisitor
	{
		private readonly Stack<string> _expression = new Stack<string>();

		//private string _expression = String.Empty;

		public string Translate(Expression expression)
		{
			Visit(expression);

			return BuildExpression(_expression);
		}

		private static string BuildExpression(Stack<string> stack)
		{
			var strategy = new ODataFilterExpressionBuilderStrategy();

			return strategy.BuildExpression(stack);
		}

		protected override Expression VisitBinary(BinaryExpression node)
		{
			string op;

			switch (node.NodeType)
			{
				case ExpressionType.Add:
					op = "add";
					break;
				case ExpressionType.AddChecked:
					op = "add";
					break;
				case ExpressionType.AndAlso:
					op = "and";
					break;
				case ExpressionType.Divide:
					op = "div";
					break;
				case ExpressionType.Equal:
					op = "eq";
					break;
				case ExpressionType.GreaterThan:
					op = "gt";
					break;
				case ExpressionType.GreaterThanOrEqual:
					op = "ge";
					break;
				case ExpressionType.LessThan:
					op = "lt";
					break;
				case ExpressionType.LessThanOrEqual:
					op = "le";
					break;
				case ExpressionType.Modulo:
					op = "mod";
					break;
				case ExpressionType.Multiply:
					op = "mul";
					break;
				case ExpressionType.MultiplyChecked:
					op = "mul";
					break;
				case ExpressionType.NotEqual:
					op = "ne";
					break;
				case ExpressionType.OrElse:
					op = "or";
					break;
				case ExpressionType.Subtract:
					op = "sub";
					break;
				case ExpressionType.SubtractChecked:
					op = "sub";
					break;
				case ExpressionType.TypeIs:
					op = "is";
					break;
				case ExpressionType.TypeEqual:
					// TODO: figure these last three operators out
					op = "+";
					break;
				case ExpressionType.IsTrue:
					op = "+";
					break;
				case ExpressionType.IsFalse:
					op = "+";
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			var result = base.VisitBinary(node);

			_expression.Push(op);
			// = String.Format("({0} {1} {2})", left, op, right);

			return result;
		}

		protected override Expression VisitConstant(ConstantExpression node)
		{
			var literal = String.Empty;

			if (node.Value == null)
			{
				literal = "Null";
			}
			else
			{
				if (node.Type == typeof(string))
				{
					literal = String.Format("'{0}'", node.Value);
				}
				else if (node.Type == typeof(Guid))
				{
					literal = String.Format("guid'{0}'", node.Value);
				}
				else if (node.Type == typeof(DateTime))
				{
					literal = String.Format("datetime'{0}'", node.Value);
				}
				else if (node.Type == typeof(TimeSpan))
				{
					literal = String.Format("time'{0}'", node.Value);
				}
				else if (node.Type == typeof(DateTimeOffset))
				{
					literal = String.Format("datetimeoffset'{0}'", node.Value);
				}
				else if (node.Type == typeof(decimal))
				{
					literal = String.Format("{0},", node.Value);
				}
				else
				{
					literal = String.Format("{0}", node.Value);
				}
			}

			_expression.Push(literal);

			return base.VisitConstant(node);
		}

		protected override Expression VisitMember(MemberExpression node)
		{
			_expression.Push(node.Member.Name);

			return base.VisitMember(node);
		}

		protected override Expression VisitParameter(ParameterExpression node)
		{
			_expression.Push(node.Name);

			return base.VisitParameter(node);
		}

		protected override Expression VisitUnary(UnaryExpression node)
		{
			return base.VisitUnary(node);
		}
	}
}
