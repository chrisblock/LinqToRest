namespace LinqToRest.OData.Literals.Impl
{
	public class DoubleRegularExpressionTableLexerEntry : AbstractRegularExpressionTableLexerEntry
	{
		public override TokenType TokenType { get { return TokenType.Double; } }

		public DoubleRegularExpressionTableLexerEntry() : base(@"(?<!\S)[+-]?(?:\d\.\d{1,16}e[+-]\d{1,3}|\d+\.\d+|\d{1,17})d?(?![\w\.])")
		{
		}
	}
}
