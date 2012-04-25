using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace LinqToRest.OData
{
	public class ODataQuery
	{
		private readonly ICollection<string> _orderByPredicates = new List<string>();

		public string Url { get; set; }
		
		public string ExpandPredicate { get; set; }
		
		public string FilterPredicate { get; set; }
		
		public ODataFormat Format { get; set; }

		public ICollection<string> OrderByPredicates { get { return _orderByPredicates; } }
		public string OrderByPredicate { get { return String.Join(", ", OrderByPredicates); } }

		public string SelectPredicate { get; set; }

		public int? Skip { get; set; }

		public string SkipToken { get; set; }

		public int? Top { get; set; }

		public ODataQuery()
		{
			// TODO: abstract this out
			Format = ODataFormat.Json;
		}

		public override string ToString()
		{
			var oDataQueryParts = new List<string>(8);

			if (String.IsNullOrWhiteSpace(ExpandPredicate) == false)
			{
				oDataQueryParts.Add(String.Format("{0}={1}", QueryParameters.Expand, ExpandPredicate));
			}

			if (String.IsNullOrWhiteSpace(FilterPredicate) == false)
			{
				oDataQueryParts.Add(String.Format("{0}={1}", QueryParameters.Filter, FilterPredicate));
			}

			oDataQueryParts.Add(String.Format("{0}={1}", QueryParameters.Format, Format.ToString().ToLowerInvariant()));

			if (OrderByPredicates.Any())
			{
				oDataQueryParts.Add(String.Format("{0}={1}", QueryParameters.OrderBy, OrderByPredicate));
			}

			if (String.IsNullOrWhiteSpace(SelectPredicate) == false)
			{
				oDataQueryParts.Add(String.Format("{0}={1}", QueryParameters.Select, SelectPredicate));
			}

			if (Skip.HasValue)
			{
				oDataQueryParts.Add(String.Format("{0}={1}", QueryParameters.Skip, Skip));
			}

			if (String.IsNullOrWhiteSpace(SkipToken) == false)
			{
				oDataQueryParts.Add(String.Format("{0}={1}", QueryParameters.SkipToken, SkipToken));
			}

			if (Top.HasValue)
			{
				oDataQueryParts.Add(String.Format("{0}={1}", QueryParameters.Top, Top));
			}

			return String.Format("{0}?{1}", Url, String.Join("&", oDataQueryParts));
		}

		public static ODataQuery Parse<T>(string queryString)
		{
			var result = new ODataQuery();

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
