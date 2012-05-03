using LinqToRest.OData.Filters;

namespace LinqToRest.OData
{
	public class ODataFilterQueryPart : ODataQuery
	{
		private readonly ODataQueryFilterExpression _filterExpression;

		public override ODataQueryPartType QueryType { get { return ODataQueryPartType.Filter; } }

		public ODataFilterQueryPart(ODataQueryFilterExpression filterExpression)
		{
			_filterExpression = filterExpression;
		}

		public override string ToString()
		{
			var result = BuildParameterString(_filterExpression.ToString());

			return result;
		}
	}
}
