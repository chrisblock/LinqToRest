using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace LinqToRest.OData
{
	public class ODataQuery
	{
		public static class ParameterTypes
		{
			public static string Expand { get { return "$expand"; } }
			public static string Filter { get { return "$filter"; } }
			public static string Format { get { return "$format"; } }
			public static string OrderBy { get { return "$orderby"; } }
			public static string Select { get { return "$select"; } }
			public static string Skip { get { return "$skip"; } }
			public static string SkipToken { get { return "$skiptoken"; } }
			public static string Top { get { return "$top"; } }
		}

		private readonly ICollection<string> _orderByPredicates = new List<string>();

		public Type ItemType { get; set; }

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
				oDataQueryParts.Add(String.Format("{0}={1}", ParameterTypes.Expand, ExpandPredicate));
			}

			if (String.IsNullOrWhiteSpace(FilterPredicate) == false)
			{
				oDataQueryParts.Add(String.Format("{0}={1}", ParameterTypes.Filter, FilterPredicate));
			}

			oDataQueryParts.Add(String.Format("{0}={1}", ParameterTypes.Format, Format.ToString().ToLowerInvariant()));

			if (OrderByPredicates.Any())
			{
				oDataQueryParts.Add(String.Format("{0}={1}", ParameterTypes.OrderBy, OrderByPredicate));
			}

			if (String.IsNullOrWhiteSpace(SelectPredicate) == false)
			{
				oDataQueryParts.Add(String.Format("{0}={1}", ParameterTypes.Select, SelectPredicate));
			}

			if (Skip.HasValue)
			{
				oDataQueryParts.Add(String.Format("{0}={1}", ParameterTypes.Skip, Skip));
			}

			if (String.IsNullOrWhiteSpace(SkipToken) == false)
			{
				oDataQueryParts.Add(String.Format("{0}={1}", ParameterTypes.SkipToken, SkipToken));
			}

			if (Top.HasValue)
			{
				oDataQueryParts.Add(String.Format("{0}={1}", ParameterTypes.Top, Top));
			}

			return String.Format("{0}?{1}", Url, String.Join("&", oDataQueryParts));
		}

		public static ODataQuery Parse<T>(string queryString)
		{
			var result = new ODataQuery();

			var matches = Regex.Matches(queryString, @"[?&](\$[^=]+)=([^&]+)").Cast<Match>();

			if (queryString.StartsWith("?"))
			{
				queryString = queryString.Substring(1);
			}

			var queryParts = queryString.Split('&');

			foreach (var queryPart in queryParts)
			{
				var parameterParts = queryPart.Split('=');
				var parameterName = parameterParts[0];
				var parameterValue = parameterParts[1];

				//switch (parameterName)
				//{
				//    case ParameterTypes.Expand:
				//        break;
				//}
			}

			return result;
		}
	}
}
