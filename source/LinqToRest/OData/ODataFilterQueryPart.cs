using LinqToRest.OData.Filters;

namespace LinqToRest.OData
{
	public class ODataFilterQueryPart : ODataQuery
	{
		public override ODataQueryPartType QueryType { get { return ODataQueryPartType.Filter; } }

		public ODataQueryFilterExpression FilterExpression { get; private set; }

		public ODataFilterQueryPart(ODataQueryFilterExpression filterExpression)
		{
			FilterExpression = filterExpression;
		}

		public override string ToString()
		{
			var result = BuildParameterString(FilterExpression.ToString());

			return result;
		}
	}
}
