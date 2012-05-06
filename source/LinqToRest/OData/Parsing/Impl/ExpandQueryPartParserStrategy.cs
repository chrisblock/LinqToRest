namespace LinqToRest.OData.Parsing.Impl
{
	public class ExpandQueryPartParserStrategy : AbstractQueryPartParserStrategy<ExpandQueryPart>
	{
		public ExpandQueryPartParserStrategy() : base(ODataQueryPartType.Expand)
		{
		}

		protected override ExpandQueryPart Parse(string parameterValue)
		{
			return ODataQueryPart.Expand(parameterValue);
		}
	}
}
