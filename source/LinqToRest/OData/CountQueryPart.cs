namespace LinqToRest.OData
{
	public class CountQueryPart : ODataQueryPart
	{
		public override ODataQueryPartType QueryPartType { get { return ODataQueryPartType.Count; } }

		public override string ToString()
		{
			return QueryPartType.GetUrlParameterName();
		}
	}
}
