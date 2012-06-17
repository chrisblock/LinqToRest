namespace LinqToRest.OData.Literals.Impl
{
	public class PrimitiveLiteral : AbstractLiteral
	{
		public PrimitiveLiteral() : base(@"(?:edm\.)?binary|boolean|byte|datetime|decimal|double|single|float|guid|int16|int32|int64|sbyte|string|time|datetimeoffset|stream")
		{
		}
	}
}
