using System;
using System.Collections.Generic;

using LinqToRest.OData;
using LinqToRest.OData.Literals.Impl;

namespace LinqToRest.Server.OData.Parsing.Impl
{
	public class ODataQueryPartParserStrategy : IODataQueryPartParserStrategy
	{
		private readonly IDictionary<ODataQueryPartType, IODataQueryPartParserStrategy> _strategies;

		public ODataQueryPartParserStrategy()
		{
			_strategies = new Dictionary<ODataQueryPartType, IODataQueryPartParserStrategy>
			{
				{ ODataQueryPartType.Count, new CountQueryPartParserStrategy() },
				{ ODataQueryPartType.Expand, new ExpandQueryPartParserStrategy() },
				{ ODataQueryPartType.Filter, new FilterQueryPartParserStrategy(new RegularExpressionTableLexer()) },
				{ ODataQueryPartType.Format, new FormatQueryPartParserStrategy() },
				{ ODataQueryPartType.InlineCount, new InlineCountQueryPartParserStrategy() },
				{ ODataQueryPartType.OrderBy, new OrderByQueryPartParserStrategy() },
				{ ODataQueryPartType.Select, new SelectQueryPartParserStrategy() },
				{ ODataQueryPartType.Skip, new SkipQueryPartParserStrategy() },
				{ ODataQueryPartType.SkipToken, new SkipTokenQueryPartParserStrategy() },
				{ ODataQueryPartType.Top, new TopQueryPartParserStrategy() }
			};
		}

		public ODataQueryPart Parse(ODataQueryPartType type, string parameterValue)
		{
			IODataQueryPartParserStrategy strategy;
			if (_strategies.TryGetValue(type, out strategy) == false)
			{
				throw new ArgumentException(String.Format("There is no parsing strategy for ODataQueryPartType '{0}'.", type));
			}

			var result = strategy.Parse(type, parameterValue);

			return result;
		}
	}
}
