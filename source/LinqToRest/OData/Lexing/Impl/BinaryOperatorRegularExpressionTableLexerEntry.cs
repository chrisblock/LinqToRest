using System;
using System.Collections.Generic;
using System.Linq;

using LinqToRest.OData.Filters;

namespace LinqToRest.OData.Lexing.Impl
{
	public class BinaryOperatorRegularExpressionTableLexerEntry : AbstractRegularExpressionTableLexerEntry
	{
		public static readonly IEnumerable<string> Operators = Enum.GetValues(typeof(FilterExpressionOperator))
			.Cast<FilterExpressionOperator>()
			.Where(x => x != FilterExpressionOperator.Unknown)
			.Where(x => x != FilterExpressionOperator.Not)
			.Where(x => x != FilterExpressionOperator.Negate)
			.Select(x => x.GetODataQueryOperatorString());

		public override TokenType TokenType { get { return TokenType.BinaryOperator; } }

		public BinaryOperatorRegularExpressionTableLexerEntry() : base(String.Format(@"\b(?:{0})\b", String.Join("|", Operators)))
		{
		}
	}
}
