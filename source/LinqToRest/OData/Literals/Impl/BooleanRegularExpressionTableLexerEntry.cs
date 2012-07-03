namespace LinqToRest.OData.Literals.Impl
{
	public class BooleanRegularExpressionTableLexerEntry : AbstractRegularExpressionTableLexerEntry
	{
		public override TokenType TokenType { get { return TokenType.Boolean; } }

		public BooleanRegularExpressionTableLexerEntry() : base(@"\b(?:true|false)\b")
		{
		}
	}
}
