namespace LinqToRest.OData.Literals.Impl
{
	public class CommaRegularExpressionTableLexerEntry : AbstractRegularExpressionTableLexerEntry
	{
		public override TokenType TokenType { get { return TokenType.Comma; } }

		public CommaRegularExpressionTableLexerEntry() : base(@",")
		{
		}
	}
}
