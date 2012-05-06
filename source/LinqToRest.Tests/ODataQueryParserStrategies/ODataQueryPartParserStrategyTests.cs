using LinqToRest.OData;
using LinqToRest.OData.Parsing.Impl;

using NUnit.Framework;

namespace LinqToRest.Tests.ODataQueryParserStrategies
{
	[TestFixture]
	public class ODataQueryPartParserStrategyTests
	{
		[Test]
		public void Parse_QueryPartTypeWithoutAParsingStrategy_ThrowsArgumentException()
		{
			var strategy = new ODataQueryPartParserStrategy();

			Assert.That(() => strategy.Parse(ODataQueryPartType.Ordering, ""), Throws.ArgumentException);
		}
	}
}
