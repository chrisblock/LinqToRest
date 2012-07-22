using LinqToRest.OData.Lexing;

namespace LinqToRest.Server.OData.Lexing.Impl
{
	public class PrimitiveRegularExpressionTableLexerEntry : AbstractRegularExpressionTableLexerEntry
	{
		public override TokenType TokenType { get { return TokenType.Primitive; } }

		public PrimitiveRegularExpressionTableLexerEntry() : base(@"(?:edm\.)?(?:binary|boolean|byte|datetime|decimal|double|single|float|guid|int16|int32|int64|sbyte|string|time|datetimeoffset|stream)")
		{
		}
	}
}
