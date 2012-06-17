namespace LinqToRest.OData.Literals.Impl
{
	public class TimeLiteral : AbstractLiteral
	{
		public TimeLiteral() : base(@"time'([^']+)'")
		{
		}
	}
}
