namespace LinqToRest.OData.Literals.Impl
{
	public class DateTimeOffsetRegularExpressionTableLexerEntry : AbstractRegularExpressionTableLexerEntry
	{
		public override TokenType TokenType { get { return TokenType.DateTimeOffset; } }

		public DateTimeOffsetRegularExpressionTableLexerEntry() : base(@"\bdatetimeoffset'\d{4}\-[0-2]\d-[0-3]\dT[0-2]\d:[0-6]\d(?::[0-6]\d(?:\.\d{3})?)?(?:Z|(?:[+\-]\d\d:\d\d))'")
		{
		}
	}
}
