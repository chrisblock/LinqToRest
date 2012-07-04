namespace LinqToRest.OData.Literals.Impl
{
	public class DateTimeRegularExpressionTableLexerEntry : AbstractRegularExpressionTableLexerEntry
	{
		public override TokenType TokenType { get { return TokenType.DateTime; } }

		public DateTimeRegularExpressionTableLexerEntry() : base(@"\bdatetime'\d{4}\-[0-2]\d-[0-3]\dT[0-2]\d:[0-6]\d(?::[0-6]\d(?:\.\d{3,6})?)?'")
		{
		}
	}
}
