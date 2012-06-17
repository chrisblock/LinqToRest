namespace LinqToRest.OData.Literals.Impl
{
	public class DateTimeLiteral : AbstractLiteral
	{
		public DateTimeLiteral() : base(@"datetime'\d{4}\-[0-2]\d-[0-3]\dT[0-2]\d:[0-6]\d(?::[0-6](?:\.\d{3})?)?'")
		{
		}
	}
}
