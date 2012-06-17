using System;

using LinqToRest.OData;

namespace LinqToRest.Server.OData.Parsing.Impl
{
	public class InlineCountQueryPartParserStrategy : AbstractQueryPartParserStrategy<InlineCountQueryPart>
	{
		public InlineCountQueryPartParserStrategy() : base(ODataQueryPartType.InlineCount)
		{
		}

		protected override InlineCountQueryPart Parse(string parameterValue)
		{
			InlineCountType countType;

			if (Enum.TryParse(parameterValue, true, out countType) == false)
			{
				throw new ArgumentException(String.Format("'{0}' not recognized as an inline count type.", parameterValue));
			}

			return ODataQueryPart.InlineCount(countType);
		}
	}
}
