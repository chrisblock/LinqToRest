namespace LinqToRest.OData.Parsing.Impl
{
	public class CountQueryPartParserStrategy : AbstractQueryPartParserStrategy
	{
		public CountQueryPartParserStrategy() : base(ODataQueryPartType.Count)
		{
		}

		protected override ODataQuery Parse(string parameterValue)
		{
			return ODataQuery.Count();
		}
	}
}
