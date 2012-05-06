namespace LinqToRest.OData.Impl
{
	public class DefaultODataQueryFactory : IODataQueryFactory
	{
		public ODataQuery Create()
		{
			return new ODataQuery
			{
				FormatPredicate = ODataQueryPart.Format(ODataFormat.Json)
			};
		}
	}
}
