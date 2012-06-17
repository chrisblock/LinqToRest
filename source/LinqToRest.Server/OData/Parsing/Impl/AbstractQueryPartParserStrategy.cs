using System;

using LinqToRest.OData;

namespace LinqToRest.Server.OData.Parsing.Impl
{
	public abstract class AbstractQueryPartParserStrategy<T> : IODataQueryPartParserStrategy
		where T : ODataQueryPart
	{
		protected ODataQueryPartType QueryPartType { get; private set; }

		protected AbstractQueryPartParserStrategy(ODataQueryPartType queryPartType)
		{
			QueryPartType = queryPartType;
		}

		protected abstract T Parse(string parameteraValue);

		public ODataQueryPart Parse(ODataQueryPartType type, string parameterValue)
		{
			if (type != QueryPartType)
			{
				throw new ArgumentException(String.Format("'{0}' cannot parse query parts of type '{1}'.", GetType(), type));
			}

			return Parse(parameterValue);
		}
	}
}
