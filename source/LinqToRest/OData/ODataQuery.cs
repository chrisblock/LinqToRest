using System;

using LinqToRest.OData.Filters;

namespace LinqToRest.OData
{
	public abstract class ODataQuery
	{
		public abstract ODataQueryPartType QueryType { get; }

		protected string BuildParameterString(object parameterValue)
		{
			return String.Format("{0}={1}", QueryType.GetUrlParameterName(), parameterValue);
		}

		public abstract override string ToString();

		public static ODataCountQueryPart Count()
		{
			return new ODataCountQueryPart();
		}

		public static ODataExpandQueryPart Expand(string predicate)
		{
			return new ODataExpandQueryPart(predicate);
		}

		public static ODataFilterQueryPart Filter(ODataQueryFilterExpression filterExpression)
		{
			return new ODataFilterQueryPart(filterExpression);
		}

		public static ODataFormatQueryPart Format(ODataFormat format)
		{
			return new ODataFormatQueryPart(format);
		}

		public static ODataInlineCountQueryPart InlineCount(InlineCountType inlineCountType)
		{
			return new ODataInlineCountQueryPart(inlineCountType);
		}

		public static ODataOrderByQueryPart OrderBy(params ODataOrdering[] orderings)
		{
			return  new ODataOrderByQueryPart(orderings);
		}

		public static ODataSelectQueryPart Select(params ODataQueryMemberAccessFilterExpression[] selectors)
		{
			return new ODataSelectQueryPart(selectors);
		}

		public static ODataSkipQueryPart Skip(int? numberToSkip)
		{
			return new ODataSkipQueryPart(numberToSkip);
		}

		public static ODataSkipTokenQueryPart SkipToken(string predicate)
		{
			return new ODataSkipTokenQueryPart(predicate);
		}

		public static ODataTopQueryPart Top(int? numberToTake)
		{
			return new ODataTopQueryPart(numberToTake);
		}

		public static ODataOrdering Ordering(string fieldName, ODataOrderingDirection direction)
		{
			return new ODataOrdering(fieldName, direction);
		}
	}
}
