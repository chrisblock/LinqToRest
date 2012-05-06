using System;
using System.Collections.Generic;

namespace LinqToRest.OData
{
	public class CompleteODataQuery : ODataQuery
	{
		public Uri Uri { get; set; }

		public ODataCountQueryPart CountPredicate { get; set; }

		public ODataExpandQueryPart ExpandPredicate { get; set; }
		
		public ODataFilterQueryPart FilterPredicate { get; set; }
		
		public ODataFormatQueryPart FormatPredicate { get; set; }

		public ODataInlineCountQueryPart InlineCountPredicate { get; set; }

		public ODataOrderByQueryPart OrderByPredicate { get; set; }

		public ODataSelectQueryPart SelectPredicate { get; set; }

		public ODataSkipQueryPart SkipPredicate { get; set; }

		// TODO: this may have been removed from the standard...
		public ODataSkipTokenQueryPart SkipTokenPredicate { get; set; }

		public ODataTopQueryPart TopPredicate { get; set; }

		public override ODataQueryPartType QueryType { get { return ODataQueryPartType.Complete; } }

		public override string ToString()
		{
			var oDataQueryParts = new List<string>(10);

			if (CountPredicate != null)
			{
				oDataQueryParts.Add(CountPredicate.ToString());
			}

			if (ExpandPredicate != null)
			{
				oDataQueryParts.Add(ExpandPredicate.ToString());
			}

			if (FilterPredicate != null)
			{
				oDataQueryParts.Add(FilterPredicate.ToString());
			}

			oDataQueryParts.Add(FormatPredicate.ToString());

			if (OrderByPredicate != null)
			{
				oDataQueryParts.Add(OrderByPredicate.ToString());
			}

			if (SelectPredicate != null)
			{
				oDataQueryParts.Add(SelectPredicate.ToString());
			}

			if (SkipPredicate != null)
			{
				oDataQueryParts.Add(SkipPredicate.ToString());
			}

			if (SkipTokenPredicate != null)
			{
				oDataQueryParts.Add(SkipTokenPredicate.ToString());
			}

			if (TopPredicate != null)
			{
				oDataQueryParts.Add(TopPredicate.ToString());
			}

			if (InlineCountPredicate != null)
			{
				oDataQueryParts.Add(InlineCountPredicate.ToString());
			}

			return String.Format("{0}?{1}", Uri, String.Join("&", oDataQueryParts));
		}
	}

	public class ODataCountQueryPart : ODataQuery
	{
		public override ODataQueryPartType QueryType { get { return ODataQueryPartType.Count; } }

		public override string ToString()
		{
			return QueryType.GetUrlParameterName();
		}
	}
}
