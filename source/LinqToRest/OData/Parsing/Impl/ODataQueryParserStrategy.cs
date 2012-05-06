using System;
using System.Collections.Generic;

namespace LinqToRest.OData.Parsing.Impl
{
	public class ODataQueryParserStrategy : IODataQueryParserStrategy
	{
		private readonly IDictionary<ODataQueryPartType, IODataQueryParserStrategy> _strategies;

		public ODataQueryParserStrategy()
		{
			_strategies = new Dictionary<ODataQueryPartType, IODataQueryParserStrategy>
			{
				{ ODataQueryPartType.Count, new CountQueryPartParserStrategy() },
				{ ODataQueryPartType.Expand, new ExpandQueryPartParserStrategy() },
				{ ODataQueryPartType.Filter, new FilterQueryPartParserStrategy() },
				{ ODataQueryPartType.Format, new FormatQueryPartParserStrategy() },
				{ ODataQueryPartType.InlineCount, new InlineCountQueryPartParserStrategy() },
				{ ODataQueryPartType.OrderBy, new OrderByQueryPartParserStrategy() },
				{ ODataQueryPartType.Select, new SelectQueryPartParserStrategy() },
				{ ODataQueryPartType.Skip, new SkipQueryPartParserStrategy() },
				{ ODataQueryPartType.SkipToken, new SkipTokenQueryPartParserStrategy() },
				{ ODataQueryPartType.Top, new TopQueryPartParserStrategy() }
			};
		}

		public ODataQuery Parse(ODataQueryPartType type, string parameterValue)
		{
			IODataQueryParserStrategy strategy;
			if (_strategies.TryGetValue(type, out strategy) == false)
			{
				throw new ArgumentException(String.Format("There is no parsing strategy for ODataQueryPartType '{0}'.", type));
			}

			var result = strategy.Parse(type, parameterValue);

			return result;
		}
	}
}
