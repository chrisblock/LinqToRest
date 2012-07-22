using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using LinqToRest.OData.Filters;
using LinqToRest.OData.Lexing;

namespace LinqToRest.Server.OData.Lexing.Impl
{
	public class UnaryOperatorRegularExpressionTableLexerEntry : AbstractRegularExpressionTableLexerEntry
	{
		public static readonly IEnumerable<string> Operators = new List<string>
		{
			FilterExpressionOperator.Not.GetODataQueryOperatorString(),
			Regex.Escape(FilterExpressionOperator.Negate.GetODataQueryOperatorString())
		};

		public override TokenType TokenType { get { return TokenType.UnaryOperator; } }

		// TODO: i think this will cause issue for the negation operator (e.g. it won't match '-TestInt')
		public UnaryOperatorRegularExpressionTableLexerEntry() : base(String.Format(@"(?<!\w)(?:{0})(?!\w)", String.Join("|", Operators)))
		{
		}
	}
}
