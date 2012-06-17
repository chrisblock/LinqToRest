namespace LinqToRest.OData.Literals.Impl
{
	public class ShortLiteral : AbstractLiteral
	{
		public ShortLiteral() : base(@"\-?\d{1,5}")
		{
		}
	}
}
