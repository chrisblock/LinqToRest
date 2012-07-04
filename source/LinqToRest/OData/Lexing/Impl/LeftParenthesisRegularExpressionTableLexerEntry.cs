namespace LinqToRest.OData.Lexing.Impl
{
	public class LeftParenthesisRegularExpressionTableLexerEntry : AbstractRegularExpressionTableLexerEntry
	{
		public override TokenType TokenType { get { return TokenType.LeftParenthesis; } }

		public LeftParenthesisRegularExpressionTableLexerEntry() : base(@"\(")
		{
		}
	}
}
