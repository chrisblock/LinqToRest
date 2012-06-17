using LinqToRest.OData;

namespace LinqToRest.Server.OData.Parsing
{
	public interface IODataQueryPartParserStrategy
	{
		ODataQueryPart Parse(ODataQueryPartType type, string parameterValue);
	}
}
