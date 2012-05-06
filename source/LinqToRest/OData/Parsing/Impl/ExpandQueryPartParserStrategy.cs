namespace LinqToRest.OData.Parsing.Impl
{
	public class ExpandQueryPartParserStrategy : AbstractQueryPartParserStrategy
	{
		public ExpandQueryPartParserStrategy() : base(ODataQueryPartType.Expand)
		{
		}

		protected override ODataQuery Parse(string parameterValue)
		{
			return ODataQuery.Expand(parameterValue);
		}
	}
}
