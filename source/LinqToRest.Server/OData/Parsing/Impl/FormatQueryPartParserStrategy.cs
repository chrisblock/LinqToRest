using System;

using LinqToRest.OData;

namespace LinqToRest.Server.OData.Parsing.Impl
{
	public class FormatQueryPartParserStrategy : AbstractQueryPartParserStrategy<FormatQueryPart>
	{
		public FormatQueryPartParserStrategy() : base(ODataQueryPartType.Format)
		{
		}

		protected override FormatQueryPart Parse(string parameterValue)
		{
			ODataFormat format;

			if (Enum.TryParse(parameterValue, true, out format) == false)
			{
				throw new ArgumentException(String.Format("'{0}' is not a recognized OData format specification.", parameterValue));
			}

			return ODataQueryPart.Format(format);
		}
	}
}
