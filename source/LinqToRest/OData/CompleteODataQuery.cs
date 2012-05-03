using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace LinqToRest.OData
{
	public class CompleteODataQuery : ODataQuery
	{
		public Uri Uri { get; set; }
		
		public ODataExpandQueryPart ExpandPredicate { get; set; }
		
		public ODataFilterQueryPart FilterPredicate { get; set; }
		
		public ODataFormatQueryPart DataFormat { get; set; }

		public ODataOrderByQueryPart OrderByPredicate { get; set; }

		public ODataSelectQueryPart SelectPredicate { get; set; }

		public ODataSkipQueryPart SkipPredicate { get; set; }

		public ODataSkipTokenQueryPart SkipTokenPredicate { get; set; }

		public ODataTopQueryPart TopPredicate { get; set; }

		public CompleteODataQuery()
		{
			// TODO: abstract this out
			DataFormat = Format(ODataFormat.Json);
		}

		public override ODataQueryPartType QueryType { get { return ODataQueryPartType.Complete; } }

		public override string ToString()
		{
			var oDataQueryParts = new List<string>(8);

			if (ExpandPredicate != null)
			{
				oDataQueryParts.Add(ExpandPredicate.ToString());
			}

			if (FilterPredicate != null)
			{
				oDataQueryParts.Add(FilterPredicate.ToString());
			}

			oDataQueryParts.Add(DataFormat.ToString());

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

			return String.Format("{0}?{1}", Uri, String.Join("&", oDataQueryParts));
		}

		public static CompleteODataQuery Parse<T>(string queryString)
		{
			var result = new CompleteODataQuery();

			var matches = Regex.Matches(queryString, @"[?&]([^=]+)=([^&]+)").Cast<Match>();

			foreach (var match in matches)
			{
				var groups = match.Groups.Cast<Group>().Skip(1).ToList();
				var parameterName = groups[1];
				var parameterValue = groups[2];
			}

			return result;
		}
	}
}
