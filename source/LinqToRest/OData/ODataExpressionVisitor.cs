using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using LinqToRest.OData.Strategies;

using Remotion.Linq;

namespace LinqToRest.OData
{
	public class ODataExpressionVisitor : ExpressionVisitor
	{
		private static readonly IDictionary<ExpressionType, string> ExpressionToOperator = new Dictionary<ExpressionType, string>
		{
			{ExpressionType.Add, FilterOperators.Add},
			{ExpressionType.AddChecked, FilterOperators.Add},
			{ExpressionType.And, FilterOperators.And}, // do we really need to include bitwise operators?
			{ExpressionType.AndAlso, FilterOperators.And},
			{ExpressionType.Divide, FilterOperators.Divide},
			{ExpressionType.Equal, FilterOperators.Equal},
			{ExpressionType.GreaterThan, FilterOperators.GreaterThan},
			{ExpressionType.GreaterThanOrEqual, FilterOperators.GreaterThanOrEqual},
			{ExpressionType.LessThan, FilterOperators.LessThan},
			{ExpressionType.LessThanOrEqual, FilterOperators.LessThanOrEqual},
			{ExpressionType.Modulo, FilterOperators.Modulo},
			{ExpressionType.Multiply, FilterOperators.Multiply},
			{ExpressionType.MultiplyChecked, FilterOperators.Multiply},
			{ExpressionType.NotEqual, FilterOperators.NotEqual},
			{ExpressionType.Or, FilterOperators.Or}, // do we really need to include bitwise operators?
			{ExpressionType.OrElse, FilterOperators.Or},
			{ExpressionType.Subtract, FilterOperators.Subtract},
			{ExpressionType.SubtractChecked, FilterOperators.Subtract}
		};

		private static readonly IDictionary<string, string> DateFunctions = new Dictionary<string, string>
		{
			{ DateTime.Now.GetMemberInfo(x => x.Year).Name, FilterFunctions.Year },
			{ DateTime.Now.GetMemberInfo(x => x.Month).Name, FilterFunctions.Month },
			{ DateTime.Now.GetMemberInfo(x => x.Day).Name, FilterFunctions.Day },
			{ DateTime.Now.GetMemberInfo(x => x.Hour).Name, FilterFunctions.Hour },
			{ DateTime.Now.GetMemberInfo(x => x.Minute).Name, FilterFunctions.Minute },
			{ DateTime.Now.GetMemberInfo(x => x.Second).Name, FilterFunctions.Second }
		};

		private static readonly IDictionary<string, string> StringFunctions = new Dictionary<string, string>
		{
			{ ReflectionUtility.GetMethod(() => String.Empty.Replace(String.Empty, String.Empty)).Name, FilterFunctions.Replace },
			{ ReflectionUtility.GetMethod(() => String.Empty.IndexOf(String.Empty)).Name, FilterFunctions.IndexOf },
			{ ReflectionUtility.GetMethod(() => String.Empty.Substring(1)).Name, FilterFunctions.Substring },
			{ ReflectionUtility.GetMethod(() => String.Empty.StartsWith(String.Empty)).Name, FilterFunctions.StartsWith },
			{ ReflectionUtility.GetMethod(() => String.Empty.EndsWith(String.Empty)).Name, FilterFunctions.EndsWith },
			{ ReflectionUtility.GetMethod(() => String.Empty.Contains(String.Empty)).Name, FilterFunctions.SubstringOf },
			{ ReflectionUtility.GetMethod(() => String.Empty.Trim()).Name, FilterFunctions.Trim },
			{ ReflectionUtility.GetMethod(() => String.Empty.ToUpper()).Name, FilterFunctions.ToUpper },
			{ ReflectionUtility.GetMethod(() => String.Empty.ToUpperInvariant()).Name, FilterFunctions.ToUpper },
			{ ReflectionUtility.GetMethod(() => String.Empty.ToLower()).Name, FilterFunctions.ToLower },
			{ ReflectionUtility.GetMethod(() => String.Empty.ToLowerInvariant()).Name, FilterFunctions.ToLower }
		};

		private static readonly IDictionary<string, string> MathFunctions = new Dictionary<string, string>
		{
			{ ReflectionUtility.GetMethod(() => Math.Ceiling(1.0)).Name, FilterFunctions.Ceiling },
			{ ReflectionUtility.GetMethod(() => Math.Floor(1.0)).Name, FilterFunctions.Floor },
			{ ReflectionUtility.GetMethod(() => Math.Round(1.0)).Name, FilterFunctions.Round }
		};

		private static readonly IDictionary<Type, Func<object, string>> TypeFormatters = new Dictionary<Type, Func<object, string>>
		{
			{ typeof(string), obj => String.Format("'{0}'", obj) },
			{ typeof(Guid), obj => String.Format("guid'{0}'", obj) },
			{ typeof(DateTime), obj => String.Format("datetime'{0:yyyy-MM-ddTHH:mm:ssK}'", obj) },
			{ typeof(TimeSpan), obj => String.Format("time'{0}'", obj) },
			{ typeof(DateTimeOffset), obj => String.Format("datetimeoffset'{0}'", obj) },
			{ typeof(decimal), obj => String.Format("{0}m", obj) }
		};

		private readonly Stack<string> _expression = new Stack<string>();

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
			var result = base.VisitBinary(node);

			string op;
			if (ExpressionToOperator.TryGetValue(node.NodeType, out op))
			{
				_expression.Push(op);
			}
			else
			{
				throw new ArgumentOutOfRangeException();
			}

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
				Func<object, string> formatter;
				if (TypeFormatters.TryGetValue(node.Type, out formatter) == false)
				{
					formatter = obj => String.Format("{0}", obj);
				}

				literal = formatter(node.Value);
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
			return base.VisitExtension(node);
			//throw new NotSupportedException("Extension not supported by OData Query Filters.");
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
			Expression result;

			if ((node.Member.DeclaringType == typeof(string)) && (node.Member.Name == "Length"))
			{
				result = base.VisitMember(node);

				_expression.Push(FilterFunctions.Length);
			}
			else if (node.Member.DeclaringType == typeof(DateTime))
			{
				result = base.VisitMember(node);

				string dateFunction;
				if (DateFunctions.TryGetValue(node.Member.Name, out dateFunction))
				{
					_expression.Push(dateFunction);
				}
				else
				{
					throw new ArgumentException(String.Format("Member '{0}' of DateTime not recognized.", node.Member.Name));
				}
			}
			// TODO: this condition seems a little sketchy
			else if ((node.Expression.NodeType != ExpressionType.MemberAccess))
			{
				_expression.Push(node.Member.Name);

				result = node;
			}
			else
			{
				result = base.VisitMember(node);

				_expression.Push(node.Member.Name);

				_expression.Push("->");
			}

			return result;
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
			var result = base.VisitMethodCall(node);

			if (node.Method.DeclaringType == typeof(string))
			{
				string stringFunction;
				if (StringFunctions.TryGetValue(node.Method.Name, out stringFunction))
				{
					_expression.Push(stringFunction);
				}
				else
				{
					throw new NotSupportedException(String.Format("MethodCall for method '{0}' not supported by OData Query Filters.", node.Method.Name));
				}
			}
			else if (node.Method.DeclaringType == typeof(Math))
			{
				string mathFunction;
				if (MathFunctions.TryGetValue(node.Method.Name, out mathFunction))
				{
					_expression.Push(mathFunction);
				}
				else
				{
					throw new NotSupportedException(String.Format("MethodCall for method '{0}' not supported by OData Query Filters.", node.Method.Name));
				}
			}
			else
			{
				throw new NotSupportedException(String.Format("MethodCall for method '{0}' not supported by OData Query Filters.", node.Method.Name));
			}

			return result;
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
			var result = base.VisitTypeBinary(node);

			_expression.Push(node.TypeOperand.Name);

			_expression.Push("isof");

			return result;
		}

		protected override Expression VisitUnary(UnaryExpression node)
		{
			var result = base.VisitUnary(node);

			switch (node.NodeType)
			{
				case ExpressionType.Convert:
					_expression.Push(node.Type.Name);
					_expression.Push(FilterFunctions.Cast);
					break;
				case ExpressionType.TypeAs:
					_expression.Push(node.Type.Name);
					_expression.Push(FilterFunctions.Cast);
					break;
				case ExpressionType.Negate:
					_expression.Push(FilterOperators.Negate);
					break;
				case ExpressionType.NegateChecked:
					_expression.Push(FilterOperators.Negate);
					break;
				case ExpressionType.Not:
					_expression.Push(FilterOperators.Not);
					break;
				case ExpressionType.Increment:
					_expression.Push("1");
					_expression.Push(FilterOperators.Add);
					break;
				case ExpressionType.Decrement:
					_expression.Push("1");
					_expression.Push(FilterOperators.Subtract);
					break;
				// TODO: figure these two out
				//case ExpressionType.IsTrue:
				//    break;
				//case ExpressionType.IsFalse:
				//    break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			return result;
		}
	}
}
