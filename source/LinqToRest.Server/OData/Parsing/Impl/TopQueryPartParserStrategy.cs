using System;

using LinqToRest.OData;

namespace LinqToRest.Server.OData.Parsing.Impl
{
	public class TopQueryPartParserStrategy : AbstractQueryPartParserStrategy<TopQueryPart>
	{
		public TopQueryPartParserStrategy() : base(ODataQueryPartType.Top)
		{
		}

		protected override TopQueryPart Parse(string parameterValue)
		{
			int count;

			if (Int32.TryParse(parameterValue, out count) == false)
			{
				throw new ArgumentException(String.Format("Cannot take the top '{0}' number of items. '{0}' is not an integar.", parameterValue));
			}

			return ODataQueryPart.Top(count);
		}
	}
}
