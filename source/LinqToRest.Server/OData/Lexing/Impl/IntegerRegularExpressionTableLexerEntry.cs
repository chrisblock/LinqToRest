using LinqToRest.OData.Lexing;

namespace LinqToRest.Server.OData.Lexing.Impl
{
	public class IntegerRegularExpressionTableLexerEntry : AbstractRegularExpressionTableLexerEntry
	{
		public override TokenType TokenType { get { return TokenType.Integer; } }

		public IntegerRegularExpressionTableLexerEntry() : base(@"(?<!\S)[+-]?\d{1,10}(?![\w\.])")
		{
		}
	}
}
