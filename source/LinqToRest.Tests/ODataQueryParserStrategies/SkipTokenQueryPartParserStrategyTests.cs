using LinqToRest.OData;
using LinqToRest.OData.Parsing;
using LinqToRest.OData.Parsing.Impl;

using NUnit.Framework;

namespace LinqToRest.Tests.ODataQueryParserStrategies
{
	[TestFixture]
	public class SkipTokenQueryPartParserStrategyTests
	{
		private const ODataQueryPartType Type = ODataQueryPartType.SkipToken;
		private readonly IODataQueryParserStrategy _strategy = new SkipTokenQueryPartParserStrategy();

		[Test]
		public void Parse_IncorrectType_ThrowsArgumentException()
		{
			Assert.That(() => _strategy.Parse(ODataQueryPartType.Complete, ""), Throws.ArgumentException);
		}

		[Test]
		public void Parse_StringContainingIntegerTwentyThree_ReturnsCorrectQueryPart()
		{
			// TODO: this test doesn't actually accomplish much; $skiptoken documentation is...sparse
			var result = _strategy.Parse(Type, "23");

			Assert.That(result, Is.InstanceOf<ODataSkipTokenQueryPart>());
			Assert.That(((ODataSkipTokenQueryPart)result).Predicate, Is.Not.Null);
			Assert.That(((ODataSkipTokenQueryPart)result).Predicate, Is.EqualTo("23"));
			Assert.That(result.ToString(), Is.EqualTo("$skiptoken=23"));
		}
	}
}
