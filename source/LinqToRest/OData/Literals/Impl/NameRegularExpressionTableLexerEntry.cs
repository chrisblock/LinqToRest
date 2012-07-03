namespace LinqToRest.OData.Literals.Impl
{
	public class NameRegularExpressionTableLexerEntry : AbstractRegularExpressionTableLexerEntry
	{
		public override TokenType TokenType { get { return TokenType.Name; } }

		// TODO: should this really include all the characters in the spec? e.g. [:@\w\-\.~!$&'()*+,;=]+
		public NameRegularExpressionTableLexerEntry() : base(@"\b\w+\b")
		{
		}
	}
}
