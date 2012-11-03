using System;

using LinqToRest.OData.Lexing;

namespace LinqToRest.Server.OData.Lexing.Impl
{
	public class ByteRegularExpressionTableLexerEntry : AbstractNumericRegularExpressionTableLexerEntry
	{
		public override TokenType TokenType { get { return TokenType.Byte; } }

		public ByteRegularExpressionTableLexerEntry() : base(Byte.MinValue, Byte.MaxValue)
		{
		}
	}
}
