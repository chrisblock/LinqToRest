namespace LinqToRest.OData.Literals.Impl
{
	public class FloatRegularExpressionTableLexerEntry : AbstractRegularExpressionTableLexerEntry
	{
		public override TokenType TokenType { get { return TokenType.Float; } }

		public FloatRegularExpressionTableLexerEntry() : base(@"(?<!\S)[+-]?(?:\d\.\d{1,8}e[+-]\d{1,2}|\d+\.\d+|\d{1,8})f\b")
		{
		}
	}
}
