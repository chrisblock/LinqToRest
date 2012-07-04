namespace LinqToRest.OData.Lexing.Impl
{
	public class RightParenthesisRegularExpressionTableLexerEntry : AbstractRegularExpressionTableLexerEntry
	{
		public override TokenType TokenType { get { return TokenType.RightParenthesis; } }

		public RightParenthesisRegularExpressionTableLexerEntry() : base(@"\)")
		{
		}
	}
}
