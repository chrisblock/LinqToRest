using System.Collections.Generic;
using System.Linq;

using LinqToRest.OData;
using LinqToRest.OData.Filters;
using LinqToRest.OData.Lexing;
using LinqToRest.OData.Parsing.Impl;

namespace LinqToRest.Server.OData.Parsing.Impl
{
	public class FilterQueryPartParserStrategy : AbstractQueryPartParserStrategy<FilterQueryPart>
	{
		private static readonly IDictionary<FilterExpressionOperator, int> Precedence = new Dictionary<FilterExpressionOperator, int>
		{
			{FilterExpressionOperator.Negate, 0},
			{FilterExpressionOperator.Not, 1},

			{FilterExpressionOperator.Multiply, 2},
			{FilterExpressionOperator.Divide, 3},
			{FilterExpressionOperator.Modulo, 4},

			{FilterExpressionOperator.Add, 5},
			{FilterExpressionOperator.Subtract, 6},

			{FilterExpressionOperator.LessThan, 7},
			{FilterExpressionOperator.GreaterThan, 8},
			{FilterExpressionOperator.LessThanOrEqual, 9},
			{FilterExpressionOperator.GreaterThanOrEqual, 10},

			{FilterExpressionOperator.Equal, 11},
			{FilterExpressionOperator.NotEqual, 12},

			{FilterExpressionOperator.And, 13},
			{FilterExpressionOperator.Or,  14}
		};

		private readonly IRegularExpressionTableLexer _regularExpressionTableLexer;

		public FilterQueryPartParserStrategy(IRegularExpressionTableLexer regularExpressionTableLexer) : base(ODataQueryPartType.Filter)
		{
			_regularExpressionTableLexer = regularExpressionTableLexer;
		}

		// TODO: split this method up into meaningfully named pieces
		private static Stack<Token> ShuntingYardAlgorithm(IEnumerable<Token> tokens)
		{
			var output = new List<Token>();
			var operators = new Stack<Token>();

			foreach (var token in tokens)
			{
				if (token.TokenType == TokenType.Function)
				{
					operators.Push(token);
				}
				else if ((token.TokenType == TokenType.BinaryOperator) || (token.TokenType == TokenType.UnaryOperator))
				{
					if (operators.Any())
					{
						var o2 = operators.Peek();

						while ((o2 != null) && ((o2.TokenType == TokenType.UnaryOperator) || (o2.TokenType == TokenType.BinaryOperator)) && (Precedence[token.Value.GetFromODataQueryOperatorString()] > Precedence[o2.Value.GetFromODataQueryOperatorString()]))
						{
							output.Add(operators.Pop());

							o2 = operators.Any()
								? operators.Peek()
								: null;
						}
					}

					operators.Push(token);
				}
				else if (token.TokenType == TokenType.Comma)
				{
					while (operators.Any() && (operators.Peek().TokenType != TokenType.LeftParenthesis))
					{
						output.Add(operators.Pop());
					}
				}
				else if (token.TokenType == TokenType.LeftParenthesis)
				{
					operators.Push(token);
				}
				else if (token.TokenType == TokenType.RightParenthesis)
				{
					while (operators.Any() && (operators.Peek().TokenType != TokenType.LeftParenthesis))
					{
						output.Add(operators.Pop());
					}

					operators.Pop();

					if (operators.Any() && (operators.Peek().TokenType == TokenType.Function))
					{
						output.Add(operators.Pop());
					}
				}
				else
				{
					output.Add(token);
				}
			}

			while (operators.Any())
			{
				output.Add(operators.Pop());
			}

			return new Stack<Token>(output);
		}

		protected override FilterQueryPart Parse(string parameterValue)
		{
			var tokens = _regularExpressionTableLexer.Tokenize(parameterValue);

			var result = ShuntingYardAlgorithm(tokens);

			var builderStrategy = new FilterExpressionParserStrategy();

			var filterExpression = builderStrategy.BuildExpression(result);

			return ODataQueryPart.Filter(filterExpression);
		}
	}
}
