using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

using LinqToRest.OData.Filters;

namespace LinqToRest.OData.Parsing.Impl
{
	public class SelectQueryPartParserStrategy : AbstractQueryPartParserStrategy
	{
		public SelectQueryPartParserStrategy() : base(ODataQueryPartType.Select)
		{
		}

		protected override ODataQuery Parse(string parameterValue)
		{
			var matches = Regex.Matches(parameterValue, @"(?:\w+/)*\w+", RegexOptions.IgnoreCase).Cast<Match>();

			var selectors = new List<ODataQueryMemberAccessFilterExpression>();

			foreach (var match in matches)
			{
				var s = match.Value.Split('/');

				var selector = s
					.Aggregate<string, ODataQueryMemberAccessFilterExpression>(null, ODataQueryFilterExpression.MemberAccess);

				selectors.Add(selector);
			}

			return ODataQuery.Select(selectors.ToArray());
		}
	}
}
