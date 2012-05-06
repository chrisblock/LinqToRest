using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

using LinqToRest.OData.Building.Strategies.Impl;
using LinqToRest.OData.Filters;

namespace LinqToRest.OData.Parsing.Impl
{
	public class FilterQueryPartParserStrategy : AbstractQueryPartParserStrategy
	{
		private static readonly IEnumerable<string> Operators = Enum.GetValues(typeof(ODataQueryFilterExpressionOperator))
			.Cast<ODataQueryFilterExpressionOperator>()
			.Select(x => x.GetODataQueryOperatorString());

		private static readonly IEnumerable<string> Functions = Enum.GetValues(typeof(Function))
			.Cast<Function>()
			.Select(x => x.GetODataQueryMethodName());

		private static readonly IDictionary<ODataQueryFilterExpressionOperator, int> Precedence = new Dictionary<ODataQueryFilterExpressionOperator, int>
		{
			{ODataQueryFilterExpressionOperator.Negate, 0},
			{ODataQueryFilterExpressionOperator.Not, 1},

			{ODataQueryFilterExpressionOperator.Multiply, 2},
			{ODataQueryFilterExpressionOperator.Divide, 3},
			{ODataQueryFilterExpressionOperator.Modulo, 4},

			{ODataQueryFilterExpressionOperator.Add, 5},
			{ODataQueryFilterExpressionOperator.Subtract, 6},

			{ODataQueryFilterExpressionOperator.LessThan, 7},
			{ODataQueryFilterExpressionOperator.GreaterThan, 8},
			{ODataQueryFilterExpressionOperator.LessThanOrEqual, 9},
			{ODataQueryFilterExpressionOperator.GreaterThanOrEqual, 10},

			{ODataQueryFilterExpressionOperator.Equal, 11},
			{ODataQueryFilterExpressionOperator.NotEqual, 12},

			{ODataQueryFilterExpressionOperator.And, 13},
			{ODataQueryFilterExpressionOperator.Or,  14}
		};

		public FilterQueryPartParserStrategy() : base(ODataQueryPartType.Filter)
		{
		}

		// TODO: split this method up into meaningfully named pieces
		private static Stack<string> ShuntingYardAlgorithm(string filterExpression)
		{
			var expression = filterExpression.Trim();

			var regexes = new List<Regex>
			{
				new Regex(@"^null\b", RegexOptions.IgnoreCase),
				new Regex(@"^(?:true|false)\b", RegexOptions.IgnoreCase),
				new Regex(@"^'.*?[^\\]'", RegexOptions.IgnoreCase),
				new Regex(@"^guid'[\d\-A-F]+'", RegexOptions.IgnoreCase),
				new Regex(@"^datetime'[^']+'", RegexOptions.IgnoreCase),
				new Regex(@"^time'[^']+'", RegexOptions.IgnoreCase),
				new Regex(@"^datetimeoffset'[^']+'", RegexOptions.IgnoreCase),
				new Regex(@"^(?:\d*\.)?\d+m\b", RegexOptions.IgnoreCase),
				new Regex(@"^\d+", RegexOptions.IgnoreCase),
				new Regex(@"^(?:\d*\.)?\d+", RegexOptions.IgnoreCase),
				// TODO: there has to be a better way to match type-literals (e.g. System.String)
				new Regex(@"^(?:\w+\.)+\w+", RegexOptions.IgnoreCase)
			};

			// TODO: 'le' is matching when it should hit 'length'; matching the function regex first (below) is just a hack
			var isOperator = new Regex(String.Format(@"^(?:{0})", String.Join(@"|", Operators)), RegexOptions.IgnoreCase);
			var isFunction = new Regex(String.Format(@"^(?:{0})\b", String.Join(@"|", Functions)), RegexOptions.IgnoreCase);
			var isComma = new Regex(@"^,", RegexOptions.IgnoreCase);
			var isLeftParenthesis = new Regex(@"^\(", RegexOptions.IgnoreCase);
			var isRightParenthesis = new Regex(@"^\)", RegexOptions.IgnoreCase);

			var output = new List<string>();
			var operators = new Stack<string>();

			while (String.IsNullOrWhiteSpace(expression) == false)
			{
				var expr = expression;

				foreach (var regex in regexes)
				{
					var match = regex.Match(expression);

					if (match.Success == true)
					{
						var len = match.Value.Length;

						expression = expression.Substring(len).Trim();

						output.Add(match.Value);

						break;
					}
				}

				if (expression == expr)
				{
					if (isFunction.IsMatch(expression))
					{
						var fn = isFunction.Match(expression).Value;

						operators.Push(fn);

						expression = expression.Substring(fn.Length).Trim();
					}
					else if (isOperator.IsMatch(expression))
					{
						var o1 = isOperator.Match(expression).Value;

						if (operators.Any())
						{
							var o2 = operators.Peek();

							while ((o2 != null) && isOperator.IsMatch(o2) && (Precedence[o1.GetFromODataQueryOperatorString()] > Precedence[o2.GetFromODataQueryOperatorString()]))
							{
								output.Add(operators.Pop());

								o2 = operators.Any()
									? operators.Peek()
									: null;
							}
						}

						operators.Push(o1);

						expression = expression.Substring(o1.Length).Trim();
					}
					else if (isComma.IsMatch(expression))
					{
						var comma = isComma.Match(expression).Value;

						while (operators.Any() && (isLeftParenthesis.IsMatch(operators.Peek()) == false))
						{
							output.Add(operators.Pop());
						}

						expression = expression.Substring(comma.Length).Trim();
					}
					else if (isLeftParenthesis.IsMatch(expression))
					{
						var leftParenthesis = isLeftParenthesis.Match(expression).Value;

						operators.Push(leftParenthesis);

						expression = expression.Substring(leftParenthesis.Length).Trim();
					}
					else if (isRightParenthesis.IsMatch(expression))
					{
						var rightParenthesis = isRightParenthesis.Match(expression).Value;

						while (operators.Any() && (isLeftParenthesis.IsMatch(operators.Peek()) == false))
						{
							output.Add(operators.Pop());
						}

						operators.Pop();

						if (isFunction.IsMatch(operators.Peek()))
						{
							output.Add(operators.Pop());
						}

						expression = expression.Substring(rightParenthesis.Length).Trim();
					}
					else
					{
						var propertyName = Regex.Match(expression, @"^\w+", RegexOptions.IgnoreCase).Value;

						output.Add(propertyName);

						expression = expression.Substring(propertyName.Length).Trim();
					}
				}
			}

			while (operators.Any())
			{
				output.Add(operators.Pop());
			}

			return new Stack<string>(output);
		}

		protected override ODataQuery Parse(string parameterValue)
		{
			var result = ShuntingYardAlgorithm(parameterValue);

			var builderStrategy = new ODataFilterExpressionBuilderStrategy();

			var filterExpression = builderStrategy.BuildExpression(result);

			return ODataQuery.Filter(filterExpression);
		}
	}
}
