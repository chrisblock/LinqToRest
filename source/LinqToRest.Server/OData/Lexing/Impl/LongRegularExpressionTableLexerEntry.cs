using LinqToRest.OData.Lexing;

namespace LinqToRest.Server.OData.Lexing.Impl
{
	public class LongRegularExpressionTableLexerEntry : AbstractRegularExpressionTableLexerEntry
	{
		public override TokenType TokenType { get { return TokenType.Long; } }

		public LongRegularExpressionTableLexerEntry() : base(@"(?<!\S)[+-]?\d{1,19}L\b")
		{
		}
	}
}
