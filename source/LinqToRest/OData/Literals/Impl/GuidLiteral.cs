namespace LinqToRest.OData.Literals.Impl
{
	public class GuidLiteral : AbstractLiteral
	{
		public GuidLiteral() : base(@"guid'([0-9A-F]{8}\-[0-9A-F]{4}\-[0-9A-F]{4}\-[0-9A-F]{4}\-[0-9A-F]{12})'")
		{
		}
	}
}
