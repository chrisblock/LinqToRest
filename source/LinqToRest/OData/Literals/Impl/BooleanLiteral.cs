namespace LinqToRest.OData.Literals.Impl
{
	public class BooleanLiteral : AbstractLiteral
	{
		public BooleanLiteral() : base(@"(?:true|false)")
		{
		}
	}
}
