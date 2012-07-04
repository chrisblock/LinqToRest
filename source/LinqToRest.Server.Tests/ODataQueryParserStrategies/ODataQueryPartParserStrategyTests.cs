// ReSharper disable InconsistentNaming

using LinqToRest.OData;
using LinqToRest.Server.OData.Parsing.Impl;

using NUnit.Framework;

namespace LinqToRest.Server.Tests.ODataQueryParserStrategies
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
