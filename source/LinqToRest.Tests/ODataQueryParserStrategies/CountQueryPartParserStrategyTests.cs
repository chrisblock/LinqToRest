using LinqToRest.OData;
using LinqToRest.OData.Parsing;
using LinqToRest.OData.Parsing.Impl;

using NUnit.Framework;

namespace LinqToRest.Tests.ODataQueryParserStrategies
{
	[TestFixture]
	public class CountQueryPartParserStrategyTests
	{
		private const ODataQueryPartType Type = ODataQueryPartType.Count;
		private readonly IODataQueryParserStrategy _strategy = new CountQueryPartParserStrategy();

		[Test]
		public void Parse_IncorrectType_ThrowsArgumentException()
		{
			Assert.That(() => _strategy.Parse(ODataQueryPartType.Complete, ""), Throws.ArgumentException);
		}

		[Test]
		public void Parse_EmptyString_ReturnsCorrectQueryPart()
		{
			var result = _strategy.Parse(Type, "");

			Assert.That(result, Is.InstanceOf<ODataCountQueryPart>());
			Assert.That(result.ToString(), Is.EqualTo("$count"));
		}
	}
}
