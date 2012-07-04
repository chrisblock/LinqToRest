using System;
using System.Collections.Generic;
using System.Linq;

using LinqToRest.OData.Filters;

namespace LinqToRest.OData.Lexing.Impl
{
	public class FunctionRegularExpressionTableLexerEntry : AbstractRegularExpressionTableLexerEntry
	{
		public static readonly IEnumerable<string> Functions = Enum.GetValues(typeof(Function))
			.Cast<Function>()
			.Where(x => x != Function.Unknown)
			.Select(x => x.GetODataQueryMethodName());

		public override TokenType TokenType { get { return TokenType.Function; } }

		public FunctionRegularExpressionTableLexerEntry() : base(String.Format(@"\b(?:{0})\b", String.Join("|", Functions)))
		{
		}
	}
}
