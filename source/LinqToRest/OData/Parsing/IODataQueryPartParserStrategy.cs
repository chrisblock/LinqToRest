namespace LinqToRest.OData.Parsing
{
	public interface IODataQueryPartParserStrategy
	{
		ODataQueryPart Parse(ODataQueryPartType type, string parameterValue);
	}
}
