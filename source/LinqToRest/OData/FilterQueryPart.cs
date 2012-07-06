using System;

using LinqToRest.OData.Filters;

namespace LinqToRest.OData
{
	public class FilterQueryPart : ODataQueryPart
	{
		public override ODataQueryPartType QueryPartType { get { return ODataQueryPartType.Filter; } }

		public FilterExpression FilterExpression { get; private set; }

		public FilterQueryPart(FilterExpression filterExpression)
		{
			if (filterExpression == null)
			{
				throw new ArgumentNullException("filterExpression");
			}

			FilterExpression = filterExpression;
		}

		public override string ToString()
		{
			var result = BuildParameterString(FilterExpression.ToString());

			return result;
		}
	}
}
