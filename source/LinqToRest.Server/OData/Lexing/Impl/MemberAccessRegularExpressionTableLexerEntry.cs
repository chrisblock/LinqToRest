using LinqToRest.OData.Lexing;

namespace LinqToRest.Server.OData.Lexing.Impl
{
	public class MemberAccessRegularExpressionTableLexerEntry : AbstractRegularExpressionTableLexerEntry
	{
		public override TokenType TokenType { get { return TokenType.MemberAccess; } }

		public MemberAccessRegularExpressionTableLexerEntry() : base(@"\.")
		{
		}
	}
}
