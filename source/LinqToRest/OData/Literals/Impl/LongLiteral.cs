namespace LinqToRest.OData.Literals.Impl
{
	public class LongLiteral : AbstractLiteral
	{
		public LongLiteral() : base(@"\-?\d{1,19}L?")
		{
		}
	}
}
