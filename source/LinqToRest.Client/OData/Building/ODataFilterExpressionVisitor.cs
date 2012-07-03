using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using LinqToRest.OData.Building.Strategies;
using LinqToRest.OData.Filters;
using LinqToRest.OData.Literals;

using Remotion.Linq;

namespace LinqToRest.Client.OData.Building
{
	public class ODataFilterExpressionVisitor : ExpressionVisitor
	{
		private static readonly IDictionary<string, Function> DateFunctions = new Dictionary<string, Function>
		{
			{ DateTime.Now.GetMemberInfo(x => x.Year).Name, Function.Year },
			{ DateTime.Now.GetMemberInfo(x => x.Month).Name, Function.Month },
			{ DateTime.Now.GetMemberInfo(x => x.Day).Name, Function.Day },
			{ DateTime.Now.GetMemberInfo(x => x.Hour).Name, Function.Hour },
			{ DateTime.Now.GetMemberInfo(x => x.Minute).Name, Function.Minute },
			{ DateTime.Now.GetMemberInfo(x => x.Second).Name, Function.Second }
		};

		private static readonly IDictionary<string, Function> TimeFunctions = new Dictionary<string, Function>
		{
			//{ TimeSpan.MaxValue.GetMemberInfo(x => x.Years).Name, Function.Years },
			{ TimeSpan.MaxValue.GetMemberInfo(x => x.Days).Name, Function.Days },
			{ TimeSpan.MaxValue.GetMemberInfo(x => x.Hours).Name, Function.Hours },
			{ TimeSpan.MaxValue.GetMemberInfo(x => x.Minutes).Name, Function.Minutes },
			{ TimeSpan.MaxValue.GetMemberInfo(x => x.Seconds).Name, Function.Seconds }
		};

		private static readonly IDictionary<string, Function> StringFunctions = new Dictionary<string, Function>
		{
			{ ReflectionUtility.GetMethod(() => String.Empty.Replace(String.Empty, String.Empty)).Name, Function.Replace },
			{ ReflectionUtility.GetMethod(() => String.Empty.IndexOf(String.Empty)).Name, Function.IndexOf },
			{ ReflectionUtility.GetMethod(() => String.Empty.Substring(1)).Name, Function.Substring },
			{ ReflectionUtility.GetMethod(() => String.Empty.StartsWith(String.Empty)).Name, Function.StartsWith },
			{ ReflectionUtility.GetMethod(() => String.Empty.EndsWith(String.Empty)).Name, Function.EndsWith },
			{ ReflectionUtility.GetMethod(() => String.Empty.Contains(String.Empty)).Name, Function.SubstringOf },
			{ ReflectionUtility.GetMethod(() => String.Empty.Trim()).Name, Function.Trim },
			{ ReflectionUtility.GetMethod(() => String.Empty.ToUpper()).Name, Function.ToUpper },
			{ ReflectionUtility.GetMethod(() => String.Empty.ToUpperInvariant()).Name, Function.ToUpper },
			{ ReflectionUtility.GetMethod(() => String.Empty.ToLower()).Name, Function.ToLower },
			{ ReflectionUtility.GetMethod(() => String.Empty.ToLowerInvariant()).Name, Function.ToLower },
			{ ReflectionUtility.GetMethod(() => String.Concat(String.Empty, String.Empty)).Name, Function.Concat }
		};

		private static readonly IDictionary<string, Function> MathFunctions = new Dictionary<string, Function>
		{
			{ ReflectionUtility.GetMethod(() => Math.Ceiling(1.0)).Name, Function.Ceiling },
			{ ReflectionUtility.GetMethod(() => Math.Floor(1.0)).Name, Function.Floor },
			{ ReflectionUtility.GetMethod(() => Math.Round(1.0)).Name, Function.Round }
		};

		private static readonly IDictionary<Type, Func<object, string>> TypeFormatters = new Dictionary<Type, Func<object, string>>
		{
			{ typeof(string), obj => String.Format("'{0}'", obj) },
			{ typeof(Guid), obj => String.Format("guid'{0}'", obj) },
			{ typeof(DateTime), obj => String.Format("datetime'{0:yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'ffffff}'", obj) },
			{ typeof(TimeSpan), obj => String.Format("time'{0}'", obj) },
			{ typeof(DateTimeOffset), obj => String.Format("datetimeoffset'{0}'", obj) },
			{ typeof(decimal), obj => String.Format("{0}m", obj) }
		};

		private readonly IFilterExpressionBuilderStrategy _filterExpressionBuilderStrategy;
		private readonly Stack<Token> _expression = new Stack<Token>();

		public ODataFilterExpressionVisitor() : this(DependencyResolver.Current.GetInstance<IFilterExpressionBuilderStrategy>())
		{
		}

		public ODataFilterExpressionVisitor(IFilterExpressionBuilderStrategy filterExpressionBuilderStrategy)
		{
			_filterExpressionBuilderStrategy = filterExpressionBuilderStrategy;
		}

		public FilterExpression Translate(Expression expression)
		{
			Visit(expression);

			var expr = _filterExpressionBuilderStrategy.BuildExpression(_expression);

			return expr;
		}

		protected override Expression VisitBinary(BinaryExpression node)
		{
			var result = base.VisitBinary(node);

			var op = node.NodeType.GetFromDotNetExpressionType();

			// TODO: there needs to be a better way to accomplish plus-sign concatenation translation
			Token token;

			if ((op == FilterExpressionOperator.Add) && (node.Left.Type == typeof(string)))
			{
				token = new Token
				{
					TokenType = TokenType.Function,
					Value = Function.Concat.GetODataQueryMethodName()
				};
			}
			else
			{
				token = new Token
				{
					TokenType = TokenType.BinaryOperator,
					Value = op.GetODataQueryOperatorString()
				};
			}

			_expression.Push(token);

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
			Token token;

			if (node.Value == null)
			{
				token = new Token
				{
					TokenType = TokenType.Null,
					Value = "null"
				};
			}
			else
			{
				// TODO: format these for realz
				Func<object, string> formatter;
				if (TypeFormatters.TryGetValue(node.Type, out formatter) == false)
				{
					formatter = obj => String.Format("{0}", obj);
				}

				token = new Token
				{
					TokenType = LiteralTokenTypes.Lookup(node.Type),
					Value = formatter(node.Value)
				};
			}

			_expression.Push(token);

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

				_expression.Push(TokenType.Function, Function.Length.GetODataQueryMethodName());
			}
			else if ((node.Member.DeclaringType == typeof(DateTime)) || (node.Member.DeclaringType == typeof(DateTimeOffset)))
			{
				result = base.VisitMember(node);

				Function dateFunction;
				if (DateFunctions.TryGetValue(node.Member.Name, out dateFunction))
				{
					_expression.Push(TokenType.Function, dateFunction.GetODataQueryMethodName());
				}
				else
				{
					throw new ArgumentException(String.Format("Member '{0}' of {1} not recognized.", node.Member.Name, node.Member.DeclaringType.Name));
				}
			}
			else if (node.Member.DeclaringType == typeof(TimeSpan))
			{
				result = base.VisitMember(node);

				Function timeFunction;
				if (TimeFunctions.TryGetValue(node.Member.Name, out timeFunction))
				{
					_expression.Push(TokenType.Function, timeFunction.GetODataQueryMethodName());
				}
				else
				{
					throw new ArgumentException(String.Format("Member '{0}' of TimeSpan not recognized.", node.Member.Name));
				}
			}
			// TODO: this condition seems a little sketchy
			else if (node.Expression.NodeType != ExpressionType.MemberAccess)
			{
				_expression.Push(TokenType.Name, node.Member.Name);

				result = node;
			}
			else
			{
				result = base.VisitMember(node);

				_expression.Push(TokenType.Name, node.Member.Name);

				_expression.Push(TokenType.MemberAccess, ".");
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
				Function stringFunction;
				if (StringFunctions.TryGetValue(node.Method.Name, out stringFunction))
				{
					_expression.Push(TokenType.Function, stringFunction.GetODataQueryMethodName());
				}
				else
				{
					throw new NotSupportedException(String.Format("MethodCall for method '{0}' not supported by OData Query Filters.", node.Method.Name));
				}
			}
			else if (node.Method.DeclaringType == typeof(Math))
			{
				Function mathFunction;
				if (MathFunctions.TryGetValue(node.Method.Name, out mathFunction))
				{
					_expression.Push(TokenType.Function, mathFunction.GetODataQueryMethodName());
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
			// TODO: i don't think this is how ParameterExpressions should be handled...need some tests
			_expression.Push(TokenType.Name, node.Name);

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

			_expression.Push(TokenType.Primitive, EdmTypeNames.Lookup(node.TypeOperand));

			_expression.Push(TokenType.Function, "isof");

			return result;
		}

		protected override Expression VisitUnary(UnaryExpression node)
		{
			var result = base.VisitUnary(node);

			switch (node.NodeType)
			{
				case ExpressionType.Convert:
					_expression.Push(TokenType.Name, node.Type.Name);
					_expression.Push(TokenType.Function, Function.Cast.GetODataQueryMethodName());
					break;
				case ExpressionType.TypeAs:
					_expression.Push(TokenType.Name, node.Type.Name);
					_expression.Push(TokenType.Function, Function.Cast.GetODataQueryMethodName());
					break;
				case ExpressionType.Negate:
					_expression.Push(TokenType.UnaryOperator, FilterExpressionOperator.Negate.GetODataQueryOperatorString());
					break;
				case ExpressionType.NegateChecked:
					_expression.Push(TokenType.UnaryOperator, FilterExpressionOperator.Negate.GetODataQueryOperatorString());
					break;
				case ExpressionType.Not:
					_expression.Push(TokenType.UnaryOperator, FilterExpressionOperator.Not.GetODataQueryOperatorString());
					break;
				case ExpressionType.Increment:
					_expression.Push(TokenType.Integer, "1");
					_expression.Push(TokenType.BinaryOperator, FilterExpressionOperator.Add.GetODataQueryOperatorString());
					break;
				case ExpressionType.Decrement:
					_expression.Push(TokenType.Integer, "1");
					_expression.Push(TokenType.BinaryOperator, FilterExpressionOperator.Subtract.GetODataQueryOperatorString());
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
