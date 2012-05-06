using System;

namespace LinqToRest.OData.Parsing.Impl
{
	public class InlineCountQueryPartParserStrategy : AbstractQueryPartParserStrategy
	{
		public InlineCountQueryPartParserStrategy() : base(ODataQueryPartType.InlineCount)
		{
		}

		protected override ODataQuery Parse(string parameterValue)
		{
			InlineCountType countType;

			if (Enum.TryParse(parameterValue, true, out countType) == false)
			{
				throw new ArgumentException(String.Format("'{0}' not recognized as an inline count type.", parameterValue));
			}

			return ODataQuery.InlineCount(countType);
		}
	}
}
