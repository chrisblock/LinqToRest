namespace LinqToRest.OData.Literals.Impl
{
	public class MemberAccessRegularExpressionTableLexerEntry : AbstractRegularExpressionTableLexerEntry
	{
		public override TokenType TokenType { get { return TokenType.MemberAccess; } }

		public MemberAccessRegularExpressionTableLexerEntry() : base(@"\.")
		{
		}
	}
}
