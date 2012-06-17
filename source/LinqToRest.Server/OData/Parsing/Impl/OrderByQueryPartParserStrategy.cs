using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

using LinqToRest.OData;

namespace LinqToRest.Server.OData.Parsing.Impl
{
	public class OrderByQueryPartParserStrategy : AbstractQueryPartParserStrategy<OrderByQueryPart>
	{
		public OrderByQueryPartParserStrategy() : base(ODataQueryPartType.OrderBy)
		{
		}

		protected override OrderByQueryPart Parse(string parameterValue)
		{
			// TODO: support complex order by expressions (e.g. "$orderby=(Id mod 3) desc")
			var matches = Regex.Matches(parameterValue, @"(\w+)\s+(asc|desc)", RegexOptions.IgnoreCase).Cast<Match>();

			var orderings = new List<ODataOrdering>();

			foreach (var match in matches)
			{
				var groups = match.Groups.Cast<Group>().Skip(1).ToList();

				var property = groups[0].Value;
				var dir = groups[1].Value;

				ODataOrderingDirection direction;
				if (Enum.TryParse(dir, true, out direction) == false)
				{
					// TODO: I'm pretty sure this branch will never be taken, but...
					throw new ArgumentException(String.Format("'{0}' is not a valid ordering direction.", dir));
				}

				orderings.Add(ODataQueryPart.Ordering(property, direction));
			}

			return ODataQueryPart.OrderBy(orderings.ToArray());
		}
	}
}
