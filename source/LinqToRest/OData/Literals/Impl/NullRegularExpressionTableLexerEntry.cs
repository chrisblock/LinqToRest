namespace LinqToRest.OData.Literals.Impl
{
	public class NullRegularExpressionTableLexerEntry : AbstractRegularExpressionTableLexerEntry
	{
		public override TokenType TokenType { get { return TokenType.Null; } }

		public NullRegularExpressionTableLexerEntry() : base(@"\bnull\b(?:'[^']+')?")
		{
		}
	}
}
