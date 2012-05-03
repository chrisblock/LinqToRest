namespace LinqToRest.OData
{
	public class ODataSkipTokenQueryPart : ODataQuery
	{
		private readonly string _predicate;

		public override ODataQueryPartType QueryType { get { return ODataQueryPartType.SkipToken; } }

		public ODataSkipTokenQueryPart(string predicate)
		{
			_predicate = predicate;
		}

		public override string ToString()
		{
			throw new System.NotImplementedException();
		}
	}
}
