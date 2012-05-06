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
		private readonly IODataQueryParserStrategy _strategy = new TopQueryPartParserStrategy();

		[Test]
		public void Parse_IncorrectType_ThrowsArgumentException()
		{
			Assert.That(() => _strategy.Parse(ODataQueryPartType.Complete, ""), Throws.ArgumentException);
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

			Assert.That(result, Is.InstanceOf<ODataTopQueryPart>());
			Assert.That(((ODataTopQueryPart)result).NumberToTake, Is.Not.Null);
			Assert.That(((ODataTopQueryPart)result).NumberToTake, Is.EqualTo(10));
			Assert.That(result.ToString(), Is.EqualTo("$top=10"));
		}
	}
}
