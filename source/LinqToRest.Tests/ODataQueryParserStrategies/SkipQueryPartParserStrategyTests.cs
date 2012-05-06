using LinqToRest.OData;
using LinqToRest.OData.Parsing;
using LinqToRest.OData.Parsing.Impl;

using NUnit.Framework;

namespace LinqToRest.Tests.ODataQueryParserStrategies
{
	[TestFixture]
	public class SkipQueryPartParserStrategyTests
	{
		private const ODataQueryPartType Type = ODataQueryPartType.Skip;
		private readonly IODataQueryParserStrategy _strategy = new SkipQueryPartParserStrategy();

		[Test]
		public void Parse_IncorrectType_ThrowsArgumentException()
		{
			Assert.That(() => _strategy.Parse(ODataQueryPartType.Ordering, ""), Throws.ArgumentException);
		}

		[Test]
		public void Parse_StringContainingNonIntegerValue_ThrowsArgumentException()
		{
			Assert.That(() => _strategy.Parse(Type, "Hello, World."), Throws.ArgumentException);
		}

		[Test]
		public void Parse_StringContainingIntegerTwentyThree_ReturnsCorrectQueryPart()
		{
			var result = _strategy.Parse(Type, "23");

			Assert.That(result, Is.InstanceOf<SkipQueryPart>());
			Assert.That(((SkipQueryPart)result).NumberToSkip, Is.Not.Null);
			Assert.That(((SkipQueryPart)result).NumberToSkip, Is.EqualTo(23));
			Assert.That(result.ToString(), Is.EqualTo("$skip=23"));
		}
	}
}
