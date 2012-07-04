namespace LinqToRest.OData.Lexing.Impl
{
	public class ShortRegularExpressionTableLexerEntry : AbstractRegularExpressionTableLexerEntry
	{
		public override TokenType TokenType { get { return TokenType.Short; } }

		public ShortRegularExpressionTableLexerEntry() : base(@"(?<!\S)[+-]?\d{1,5}(?![\w\.])")
		{
		}
	}
}
