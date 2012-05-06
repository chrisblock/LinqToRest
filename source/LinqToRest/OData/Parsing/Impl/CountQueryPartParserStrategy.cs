namespace LinqToRest.OData.Parsing.Impl
{
	public class CountQueryPartParserStrategy : AbstractQueryPartParserStrategy<CountQueryPart>
	{
		public CountQueryPartParserStrategy() : base(ODataQueryPartType.Count)
		{
		}

		protected override CountQueryPart Parse(string parameterValue)
		{
			return ODataQueryPart.Count();
		}
	}
}
