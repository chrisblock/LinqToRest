using System;
using System.Collections.Generic;

namespace LinqToRest.OData
{
	public class ODataQuery
	{
		public Uri Uri { get; set; }

		public CountQueryPart CountPredicate { get; set; }

		public ExpandQueryPart ExpandPredicate { get; set; }
		
		public FilterQueryPart FilterPredicate { get; set; }
		
		public FormatQueryPart FormatPredicate { get; set; }

		public InlineCountQueryPart InlineCountPredicate { get; set; }

		public OrderByQueryPart OrderByPredicate { get; set; }

		public SelectQueryPart SelectPredicate { get; set; }

		public SkipQueryPart SkipPredicate { get; set; }

		// TODO: this may have been removed from the standard...
		public SkipTokenQueryPart SkipTokenPredicate { get; set; }

		public TopQueryPart TopPredicate { get; set; }

		//public override ODataQueryPartType QueryPartType { get { return ODataQueryPartType.Complete; } }

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
}
