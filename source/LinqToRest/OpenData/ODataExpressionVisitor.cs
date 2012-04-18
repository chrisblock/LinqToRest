using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using LinqToRest.OpenData.Strategies;

namespace LinqToRest.OpenData
{
	public class ODataExpressionVisitor : ExpressionVisitor
	{
		private readonly Stack<string> _expression = new Stack<string>();

		public string Translate(Expression expression)
		{
			var body = expression;

			if (expression.NodeType == ExpressionType.Lambda)
			{
				var lambda = (LambdaExpression) expression;
				body = lambda.Body;
			}

			Visit(body);

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

		protected override Expression VisitBlock(BlockExpression node)
		{
			//throw new NotSupportedException("Blocks are not supported by OData Query Filters.");
			return base.VisitBlock(node);
		}

		protected override CatchBlock VisitCatchBlock(CatchBlock node)
		{
			throw new NotSupportedException("Catch blocks are not supported by OData Query Filters.");
		}

		protected override Expression VisitConditional(ConditionalExpression node)
		{
			throw new NotSupportedException("Conditionals not supported by OData Query Filters.");
		}

		protected override Expression VisitConstant(ConstantExpression node)
		{
			string literal;

			if (node.Value == null)
			{
				literal = "Null";
			}
			else
			{
				// TODO: make this a hash lookup?? e.g.:
				/*
				var dictionary = new Dictionary<Type, Func<object, string>>
				{
					{typeof(string), o => String.Format("'{0}'", o)}
				};
				 */

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
					literal = String.Format("datetime'{0:yyyy-MM-ddTHH:mm:ssK}'", node.Value);
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
					literal = String.Format("{0}m", node.Value);
				}
				else
				{
					literal = String.Format("{0}", node.Value);
				}
			}

			_expression.Push(literal);

			return base.VisitConstant(node);
		}

		protected override Expression VisitDebugInfo(DebugInfoExpression node)
		{
			throw new NotSupportedException("Debug info not supported by OData Query Filters.");
		}

		protected override Expression VisitDefault(DefaultExpression node)
		{
			throw new NotSupportedException("Default not supported by OData Query Filters.");
		}

		protected override Expression VisitDynamic(DynamicExpression node)
		{
			throw new NotSupportedException("Dynamic not supported by OData Query Filters.");
		}

		protected override ElementInit VisitElementInit(ElementInit node)
		{
			throw new NotSupportedException("ElementInit not supported by OData Query Filters.");
		}

		protected override Expression VisitExtension(Expression node)
		{
			throw new NotSupportedException("Extension not supported by OData Query Filters.");
		}

		protected override Expression VisitGoto(GotoExpression node)
		{
			throw new NotSupportedException("Goto not supported by OData Query Filters.");
		}

		protected override Expression VisitIndex(IndexExpression node)
		{
			throw new NotSupportedException("Index not supported by OData Query Filters.");
		}

		protected override Expression VisitInvocation(InvocationExpression node)
		{
			throw new NotSupportedException("Invocation not supported by OData Query Filters.");
		}

		protected override Expression VisitLabel(LabelExpression node)
		{
			throw new NotSupportedException("Label not supported by OData Query Filters.");
		}

		protected override LabelTarget VisitLabelTarget(LabelTarget node)
		{
			throw new NotSupportedException("LabelTarget not supported by OData Query Filters.");
		}

		protected override Expression VisitLambda<T>(Expression<T> node)
		{
			throw new NotSupportedException(String.Format("Cannot translate a lambda ({0}) into OData Query syntax.", node));
		}

		protected override Expression VisitListInit(ListInitExpression node)
		{
			throw new NotSupportedException("ListInit not supported by OData Query Filters.");
		}

		protected override Expression VisitLoop(LoopExpression node)
		{
			throw new NotSupportedException("Loop not supported by OData Query Filters.");
		}

		protected override Expression VisitMember(MemberExpression node)
		{
			_expression.Push(node.Member.Name);

			// TODO: return the result of calling the base method?
			// should probably get it to recursively add the member access expressions to the OData Query..somehow
			return node;
		}

		protected override MemberAssignment VisitMemberAssignment(MemberAssignment node)
		{
			throw new NotSupportedException("MemberAssignment not supported by OData Query Filters.");
		}

		protected override MemberBinding VisitMemberBinding(MemberBinding node)
		{
			throw new NotSupportedException("MemberBinding not supported by OData Query Filters.");
		}

		protected override Expression VisitMemberInit(MemberInitExpression node)
		{
			throw new NotSupportedException("MemberInit not supported by OData Query Filters.");
		}

		protected override MemberListBinding VisitMemberListBinding(MemberListBinding node)
		{
			throw new NotSupportedException("MemberListBinding not supported by OData Query Filters.");
		}

		protected override MemberMemberBinding VisitMemberMemberBinding(MemberMemberBinding node)
		{
			throw new NotSupportedException("MemberMemberBinding not supported by OData Query Filters.");
		}

		protected override Expression VisitMethodCall(MethodCallExpression node)
		{
			throw new NotSupportedException("MethodCall not supported by OData Query Filters.");
		}

		protected override Expression VisitNew(NewExpression node)
		{
			throw new NotSupportedException("New not supported by OData Query Filters.");
		}

		protected override Expression VisitNewArray(NewArrayExpression node)
		{
			throw new NotSupportedException("NewArray not supported by OData Query Filters.");
		}

		protected override Expression VisitParameter(ParameterExpression node)
		{
			_expression.Push(node.Name);

			return base.VisitParameter(node);
		}

		protected override Expression VisitRuntimeVariables(RuntimeVariablesExpression node)
		{
			throw new NotSupportedException("RuntimeVariables not supported by OData Query Filters.");
		}

		protected override Expression VisitSwitch(SwitchExpression node)
		{
			throw new NotSupportedException("Switch not supported by OData Query Filters.");
		}

		protected override SwitchCase VisitSwitchCase(SwitchCase node)
		{
			throw new NotSupportedException("SwitchCase not supported by OData Query Filters.");
		}

		protected override Expression VisitTry(TryExpression node)
		{
			throw new NotSupportedException("Try not supported by OData Query Filters.");
		}

		protected override Expression VisitTypeBinary(TypeBinaryExpression node)
		{
			_expression.Push(String.Format("{0}", node.TypeOperand.Name));

			var result = base.Visit(node.Expression);

			_expression.Push("isof");

			return result;

			//throw new NotSupportedException("TypeBinary not supported by OData Query Filters.");
		}

		protected override Expression VisitUnary(UnaryExpression node)
		{
			string unaryOperator;

			switch (node.NodeType)
			{
				//case ExpressionType.Call:
				//    break;
				//case ExpressionType.Invoke:
				//    break;
				case ExpressionType.Convert:
					_expression.Push(node.Type.Name);
					unaryOperator = "cast";
					break;
				case ExpressionType.Negate:
					unaryOperator = "-";
					break;
				case ExpressionType.NegateChecked:
					unaryOperator = "-";
					break;
				case ExpressionType.Not:
					unaryOperator = "not";
					break;
				case ExpressionType.Increment:
					_expression.Push("1");
					unaryOperator = "add";
					break;
				case ExpressionType.Decrement:
					_expression.Push("1");
					unaryOperator = "sub";
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			var result = base.VisitUnary(node);

			_expression.Push(unaryOperator);

			return result;
		}
	}
}
