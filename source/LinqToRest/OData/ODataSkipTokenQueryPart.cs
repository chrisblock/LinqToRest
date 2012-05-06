namespace LinqToRest.OData
{
	public class ODataSkipTokenQueryPart : ODataQuery
	{
		public string Predicate { get; private set; }

		public override ODataQueryPartType QueryType { get { return ODataQueryPartType.SkipToken; } }

		public ODataSkipTokenQueryPart(string predicate)
		{
			Predicate = predicate;
		}

		public override string ToString()
		{
			return BuildParameterString(Predicate);
		}
	}
}
