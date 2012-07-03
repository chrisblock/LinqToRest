namespace LinqToRest.OData.Literals.Impl
{
	public class IntegerRegularExpressionTableLexerEntry : AbstractRegularExpressionTableLexerEntry
	{
		public override TokenType TokenType { get { return TokenType.Integer; } }

		public IntegerRegularExpressionTableLexerEntry() : base(@"(?<!\S)[+-]?\d{1,10}(?![\w\.])")
		{
		}
	}
}
