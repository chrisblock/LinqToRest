using System;

namespace LinqToRest.OData.Parsing.Impl
{
	public class SkipQueryPartParserStrategy : AbstractQueryPartParserStrategy<SkipQueryPart>
	{
		public SkipQueryPartParserStrategy() : base(ODataQueryPartType.Skip)
		{
		}

		protected override SkipQueryPart Parse(string parameterValue)
		{
			int count;

			if (Int32.TryParse(parameterValue, out count) == false)
			{
				throw new ArgumentException(String.Format("Cannot skip '{0}' number of items. '{0}' is not an integar.", parameterValue));
			}

			return ODataQueryPart.Skip(count);
		}
	}
}
