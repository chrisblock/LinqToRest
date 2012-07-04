namespace LinqToRest.OData.Lexing.Impl
{
	public class GuidRegularExpressionTableLexerEntry : AbstractRegularExpressionTableLexerEntry
	{
		public override TokenType TokenType { get { return TokenType.Guid; } }

		public GuidRegularExpressionTableLexerEntry() : base(@"\bguid'[0-9A-F]{8}\-[0-9A-F]{4}\-[0-9A-F]{4}\-[0-9A-F]{4}\-[0-9A-F]{12}'")
		{
		}
	}
}
