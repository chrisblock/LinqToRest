using System;

namespace LinqToRest.OData.Parsing.Impl
{
	public class FormatQueryPartParserStrategy : AbstractQueryPartParserStrategy
	{
		public FormatQueryPartParserStrategy() : base(ODataQueryPartType.Format)
		{
		}

		protected override ODataQuery Parse(string parameterValue)
		{
			ODataFormat format;

			if (Enum.TryParse(parameterValue, true, out format) == false)
			{
				throw new ArgumentException(String.Format("'{0}' is not a recognized OData format specification.", parameterValue));
			}

			return ODataQuery.Format(format);
		}
	}
}
