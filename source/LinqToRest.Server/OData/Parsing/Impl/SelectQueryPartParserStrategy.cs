using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

using LinqToRest.OData;
using LinqToRest.OData.Filters;

namespace LinqToRest.Server.OData.Parsing.Impl
{
	public class SelectQueryPartParserStrategy : AbstractQueryPartParserStrategy<SelectQueryPart>
	{
		public SelectQueryPartParserStrategy() : base(ODataQueryPartType.Select)
		{
		}

		protected override SelectQueryPart Parse(string parameterValue)
		{
			var matches = Regex.Matches(parameterValue, @"(?:\w+/)*\w+", RegexOptions.IgnoreCase).Cast<Match>();

			var selectors = new List<MemberAccessFilterExpression>();

			foreach (var match in matches)
			{
				var s = match.Value.Split('/');

				var selector = s
					.Aggregate<string, MemberAccessFilterExpression>(null, FilterExpression.MemberAccess);

				selectors.Add(selector);
			}

			return ODataQueryPart.Select(selectors.ToArray());
		}
	}
}
