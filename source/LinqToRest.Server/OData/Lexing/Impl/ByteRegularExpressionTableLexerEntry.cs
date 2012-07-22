using LinqToRest.OData.Lexing;

namespace LinqToRest.Server.OData.Lexing.Impl
{
	public class ByteRegularExpressionTableLexerEntry : AbstractRegularExpressionTableLexerEntry
	{
		public override TokenType TokenType { get { return TokenType.Byte; } }

		public ByteRegularExpressionTableLexerEntry() : base(@"(?<!\S)\d{1,3}(?![\w\.])")
		{
		}
	}
}
