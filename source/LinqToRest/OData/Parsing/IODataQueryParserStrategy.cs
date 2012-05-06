namespace LinqToRest.OData.Parsing
{
	public interface IODataQueryParserStrategy
	{
		ODataQueryPart Parse(ODataQueryPartType type, string parameterValue);
	}
}
