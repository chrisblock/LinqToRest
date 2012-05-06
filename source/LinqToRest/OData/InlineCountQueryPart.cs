namespace LinqToRest.OData
{
	public class InlineCountQueryPart : ODataQueryPart
	{
		public override ODataQueryPartType QueryPartType { get { return ODataQueryPartType.InlineCount; } }

		public InlineCountType InlineCountType { get; private set; }

		public InlineCountQueryPart(InlineCountType isCountQuery)
		{
			InlineCountType = isCountQuery;
		}

		public override string ToString()
		{
			return BuildParameterString(InlineCountType.ToString().ToLowerInvariant());
		}
	}
}
