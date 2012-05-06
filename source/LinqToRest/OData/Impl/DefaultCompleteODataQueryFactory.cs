namespace LinqToRest.OData.Impl
{
	public class DefaultCompleteODataQueryFactory : ICompleteODataQueryFactory
	{
		public CompleteODataQuery Create()
		{
			return new CompleteODataQuery
			{
				FormatPredicate = ODataQuery.Format(ODataFormat.Json)
			};
		}
	}
}
