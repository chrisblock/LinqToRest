using System;
using System.Collections.Generic;
using System.Linq;

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
				oDataQueryParts.Add(String.Format("$expand={0}", ExpandPredicate));
			}

			if (String.IsNullOrWhiteSpace(FilterPredicate) == false)
			{
				oDataQueryParts.Add(String.Format("$filter={0}", FilterPredicate));
			}

			oDataQueryParts.Add(String.Format("$format={0}", Format.ToString().ToLowerInvariant()));

			if (OrderByPredicates.Any())
			{
				oDataQueryParts.Add(String.Format("$orderby={0}", OrderByPredicate));
			}

			if (String.IsNullOrWhiteSpace(SelectPredicate) == false)
			{
				oDataQueryParts.Add(String.Format("$select={0}", SelectPredicate));
			}

			if (Skip.HasValue)
			{
				oDataQueryParts.Add(String.Format("$skip={0}", Skip));
			}

			if (String.IsNullOrWhiteSpace(SkipToken) == false)
			{
				oDataQueryParts.Add(String.Format("$skiptoken={0}", SkipToken));
			}

			if (Top.HasValue)
			{
				oDataQueryParts.Add(String.Format("$top={0}", Top));
			}

			return String.Format("{0}?{1}", Url, String.Join("&", oDataQueryParts));
		}
	}
}
