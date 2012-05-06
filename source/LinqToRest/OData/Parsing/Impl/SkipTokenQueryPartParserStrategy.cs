namespace LinqToRest.OData.Parsing.Impl
{
	public class SkipTokenQueryPartParserStrategy : AbstractQueryPartParserStrategy
	{
		public SkipTokenQueryPartParserStrategy() : base(ODataQueryPartType.SkipToken)
		{
		}

		protected override ODataQuery Parse(string parameterValue)
		{
			return ODataQuery.SkipToken(parameterValue);
		}
	}
}
