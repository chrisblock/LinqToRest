namespace LinqToRest.OData.Literals.Impl
{
	public class IntegerLiteral : AbstractLiteral
	{
		public IntegerLiteral() : base(@"\-?\d{1,10}")
		{
		}
	}
}
