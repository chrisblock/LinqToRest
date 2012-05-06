using LinqToRest.OData;
using LinqToRest.OData.Parsing;
using LinqToRest.OData.Parsing.Impl;

using NUnit.Framework;

namespace LinqToRest.Tests.ODataQueryParserStrategies
{
	[TestFixture]
	public class ExpandQueryPartParserStrategyTests
	{
		private const ODataQueryPartType Type = ODataQueryPartType.Expand;
		private readonly IODataQueryParserStrategy _strategy = new ExpandQueryPartParserStrategy();

		[Test]
		public void Parse_IncorrectType_ThrowsArgumentException()
		{
			Assert.That(() => _strategy.Parse(ODataQueryPartType.Complete, ""), Throws.ArgumentException);
		}

		[Test]
		public void Parse_StringContainingIntegerTen_ReturnsCorrectQueryPart()
		{
			var result = _strategy.Parse(Type, "TestProperty");

			Assert.That(result, Is.InstanceOf<ODataExpandQueryPart>());
			Assert.That(((ODataExpandQueryPart)result).Predicate, Is.Not.Null);
			Assert.That(((ODataExpandQueryPart)result).Predicate, Is.EqualTo("TestProperty"));
			Assert.That(result.ToString(), Is.EqualTo("$expand=TestProperty"));
		}
	}
}
