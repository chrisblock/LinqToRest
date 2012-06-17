namespace LinqToRest.OData.Literals.Impl
{
	public class DoubleLiteral : AbstractLiteral
	{
		public DoubleLiteral() : base(@"\-?\d{}(?:\.\d)?d")
		{
		}
	}
}
