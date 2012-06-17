namespace LinqToRest.OData.Literals.Impl
{
	public class DateTimeOffsetLiteral : AbstractLiteral
	{
		public DateTimeOffsetLiteral() : base(@"datetimeoffset'\d{4}\-[0-2]\d-[0-3]\dT[0-2]\d:[0-6]\d(?::[0-6](?:\.\d{3})?)?(?:Z|(?:[+\-]\d\d:\d\d))'")
		{
		}
	}
}
