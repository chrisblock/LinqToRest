using LinqToRest.OData.Lexing;

namespace LinqToRest.Server.OData.Lexing.Impl
{
	public class DecimalRegularExpressionTableLexerEntry : AbstractRegularExpressionTableLexerEntry
	{
		public override TokenType TokenType { get { return TokenType.Decimal; } }

		public DecimalRegularExpressionTableLexerEntry() : base(@"(?<!\S)[+-]?\d{1,29}(?:\.\d{1,29})?m\b")
		{
		}
	}
}
