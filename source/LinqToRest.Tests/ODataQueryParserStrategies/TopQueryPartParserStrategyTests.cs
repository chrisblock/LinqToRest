using LinqToRest.OData;
using LinqToRest.OData.Parsing;
using LinqToRest.OData.Parsing.Impl;

using NUnit.Framework;

namespace LinqToRest.Tests.ODataQueryParserStrategies
{
	[TestFixture]
	public class TopQueryPartParserStrategyTests
	{
		private const ODataQueryPartType Type = ODataQueryPartType.Top;
		private readonly IODataQueryPartParserStrategy _strategy = new TopQueryPartParserStrategy();

		[Test]
		public void Parse_IncorrectType_ThrowsArgumentException()
		{
			Assert.That(() => _strategy.Parse(ODataQueryPartType.Ordering, ""), Throws.ArgumentException);
		}

		[Test]
		public void Parse_StringContainingNonIntegerValue_ThrowsArgumentException()
		{
			Assert.That(() => _strategy.Parse(Type, "Goodbye, World."), Throws.ArgumentException);
		}

		[Test]
		public void Parse_StringContainingIntegerTen_ReturnsCorrectQueryPart()
		{
			var result = _strategy.Parse(Type, "10");

			Assert.That(result, Is.InstanceOf<TopQueryPart>());
			Assert.That(((TopQueryPart)result).NumberToTake, Is.Not.Null);
			Assert.That(((TopQueryPart)result).NumberToTake, Is.EqualTo(10));
			Assert.That(result.ToString(), Is.EqualTo("$top=10"));
		}
	}
}
