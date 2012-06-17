using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

using LinqToRest.OData;
using LinqToRest.OData.Filters;

namespace LinqToRest.Server.OData.Parsing.Impl
{
	public class ExpandQueryPartParserStrategy : AbstractQueryPartParserStrategy<ExpandQueryPart>
	{
		public ExpandQueryPartParserStrategy() : base(ODataQueryPartType.Expand)
		{
		}

		protected override ExpandQueryPart Parse(string parameterValue)
		{
			var matches = Regex.Matches(parameterValue, @"(?:\w+/)*\w+", RegexOptions.IgnoreCase).Cast<Match>();

			var members = new List<MemberAccessFilterExpression>();

			foreach (var match in matches)
			{
				var s = match.Value.Split('/');

				var selector = s
					.Aggregate<string, MemberAccessFilterExpression>(null, FilterExpression.MemberAccess);

				members.Add(selector);
			}

			return ODataQueryPart.Expand(members.ToArray());
		}
	}
}
