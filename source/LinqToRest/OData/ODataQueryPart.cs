using System;

using LinqToRest.OData.Filters;

namespace LinqToRest.OData
{
	public abstract class ODataQueryPart
	{
		public abstract ODataQueryPartType QueryPartType { get; }

		public abstract override string ToString();

		protected string BuildParameterString(object parameterValue)
		{
			return String.Format("{0}={1}", QueryPartType.GetUrlParameterName(), parameterValue);
		}

		public static CountQueryPart Count()
		{
			return new CountQueryPart();
		}

		public static ExpandQueryPart Expand(params MemberAccessFilterExpression[] members)
		{
			return new ExpandQueryPart(members);
		}

		public static FilterQueryPart Filter(FilterExpression filterExpression)
		{
			return new FilterQueryPart(filterExpression);
		}

		public static FormatQueryPart Format(ODataFormat format)
		{
			return new FormatQueryPart(format);
		}

		public static InlineCountQueryPart InlineCount(InlineCountType inlineCountType)
		{
			return new InlineCountQueryPart(inlineCountType);
		}

		public static OrderByQueryPart OrderBy(params ODataOrdering[] orderings)
		{
			return new OrderByQueryPart(orderings);
		}

		public static SelectQueryPart Select(params MemberAccessFilterExpression[] selectors)
		{
			return new SelectQueryPart(selectors);
		}

		public static SkipQueryPart Skip(int? numberToSkip)
		{
			return new SkipQueryPart(numberToSkip);
		}

		public static SkipTokenQueryPart SkipToken(string predicate)
		{
			return new SkipTokenQueryPart(predicate);
		}

		public static TopQueryPart Top(int? numberToTake)
		{
			return new TopQueryPart(numberToTake);
		}

		public static ODataOrdering Ordering(string fieldName, ODataOrderingDirection direction)
		{
			return new ODataOrdering(fieldName, direction);
		}
	}
}
