using System;
using System.Collections.Generic;
using System.Linq;

using LinqToRest.OData.Filters;

namespace LinqToRest.OData.Lexing.Impl
{
	public class UnaryOperatorRegularExpressionTableLexerEntry : AbstractRegularExpressionTableLexerEntry
	{
		public static readonly IEnumerable<string> Operators = new List<FilterExpressionOperator>
		{
			FilterExpressionOperator.Not,
			FilterExpressionOperator.Negate
		}.Select(x => x.GetODataQueryOperatorString());

		public override TokenType TokenType { get { return TokenType.BinaryOperator; } }

		public UnaryOperatorRegularExpressionTableLexerEntry() : base(String.Format(@"\b(?:{0})\b", String.Join("|", Operators)))
		{
		}
	}
}
