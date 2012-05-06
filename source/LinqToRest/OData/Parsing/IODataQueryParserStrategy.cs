namespace LinqToRest.OData.Parsing
{
	public interface IODataQueryParserStrategy
	{
		ODataQuery Parse(ODataQueryPartType type, string parameterValue);
	}
}
