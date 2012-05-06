namespace LinqToRest.OData
{
	public class ODataInlineCountQueryPart : ODataQuery
	{
		public override ODataQueryPartType QueryType { get { return ODataQueryPartType.InlineCount; } }

		public InlineCountType InlineCountType { get; private set; }

		public ODataInlineCountQueryPart(InlineCountType isCountQuery)
		{
			InlineCountType = isCountQuery;
		}

		public override string ToString()
		{
			return BuildParameterString(InlineCountType.ToString().ToLowerInvariant());
		}
	}
}
