namespace LinqToRest.OData.Parsing.Impl
{
	public class SkipTokenQueryPartParserStrategy : AbstractQueryPartParserStrategy<SkipTokenQueryPart>
	{
		public SkipTokenQueryPartParserStrategy() : base(ODataQueryPartType.SkipToken)
		{
		}

		protected override SkipTokenQueryPart Parse(string parameterValue)
		{
			return ODataQueryPart.SkipToken(parameterValue);
		}
	}
}
