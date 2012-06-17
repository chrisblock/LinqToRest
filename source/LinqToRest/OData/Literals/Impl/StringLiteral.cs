namespace LinqToRest.OData.Literals.Impl
{
	public class StringLiteral : AbstractLiteral
	{
		public StringLiteral() : base(@"'([^'\\]*(?:\\.[^'\\]*)*)'")
		{
		}
	}
}
