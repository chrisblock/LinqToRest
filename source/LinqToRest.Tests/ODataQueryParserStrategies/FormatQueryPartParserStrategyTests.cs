using LinqToRest.OData;
using LinqToRest.OData.Parsing;
using LinqToRest.OData.Parsing.Impl;

using NUnit.Framework;

namespace LinqToRest.Tests.ODataQueryParserStrategies
{
	[TestFixture]
	public class FormatQueryPartParserStrategyTests
	{
		private const ODataQueryPartType Type = ODataQueryPartType.Format;
		private readonly IODataQueryParserStrategy _strategy = new FormatQueryPartParserStrategy();

		[Test]
		public void Parse_IncorrectType_ThrowsArgumentException()
		{
			Assert.That(() => _strategy.Parse(ODataQueryPartType.Ordering, "json"), Throws.ArgumentException);
		}

		[Test]
		public void Parse_InvalidFormatValue_ThrowsArgumentException()
		{
			Assert.That(() => _strategy.Parse(Type, "wait! look over there!"), Throws.ArgumentException);
		}

		[Test]
		public void Parse_ValidFormatValue_ReturnsCorrectQueryPart()
		{
			var result = _strategy.Parse(Type, "atom");

			Assert.That(result, Is.InstanceOf<FormatQueryPart>());
			Assert.That(result.ToString(), Is.EqualTo("$format=atom"));
		}
	}
}
