namespace LinqToRest.OData.Literals.Impl
{
	public class PrimitiveCollectionRegularExpressionTableLexerEntry : AbstractRegularExpressionTableLexerEntry
	{
		public override TokenType TokenType { get { return TokenType.Primitive; } }

		public PrimitiveCollectionRegularExpressionTableLexerEntry() : base(@"collection\((?:edm\.)?(?:binary|boolean|byte|datetime|decimal|double|single|float|guid|int16|int32|int64|sbyte|string|time|datetimeoffset|stream)\)")
		{
		}
	}
}
