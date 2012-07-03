namespace LinqToRest.OData.Literals.Impl
{
	public class RightParenthesisRegularExpressionTableLexerEntry : AbstractRegularExpressionTableLexerEntry
	{
		public override TokenType TokenType { get { return TokenType.RightParenthesis; } }

		public RightParenthesisRegularExpressionTableLexerEntry() : base(@"\)")
		{
		}
	}
}
