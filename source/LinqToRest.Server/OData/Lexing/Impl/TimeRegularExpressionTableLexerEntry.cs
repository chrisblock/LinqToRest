using LinqToRest.OData.Lexing;

namespace LinqToRest.Server.OData.Lexing.Impl
{
	public class TimeRegularExpressionTableLexerEntry : AbstractRegularExpressionTableLexerEntry
	{
		public override TokenType TokenType { get { return TokenType.Time; } }

		public TimeRegularExpressionTableLexerEntry() : base(@"\btime'[+-]?P\d+Y\d+M\d+DT\d+H\d+M\d+(?:\.\d+)?S'")
		{
		}
	}
}
