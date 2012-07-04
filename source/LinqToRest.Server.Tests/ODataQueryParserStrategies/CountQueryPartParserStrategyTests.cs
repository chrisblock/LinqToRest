// ReSharper disable InconsistentNaming

using LinqToRest.OData;
using LinqToRest.Server.OData.Parsing;
using LinqToRest.Server.OData.Parsing.Impl;

using NUnit.Framework;

namespace LinqToRest.Server.Tests.ODataQueryParserStrategies
{
	[TestFixture]
	public class CountQueryPartParserStrategyTests
	{
		private const ODataQueryPartType Type = ODataQueryPartType.Count;
		private readonly IODataQueryPartParserStrategy _strategy = new CountQueryPartParserStrategy();

		[Test]
		public void Parse_IncorrectType_ThrowsArgumentException()
		{
			Assert.That(() => _strategy.Parse(ODataQueryPartType.Ordering, ""), Throws.ArgumentException);
		}

		[Test]
		public void Parse_EmptyString_ReturnsCorrectQueryPart()
		{
			var result = _strategy.Parse(Type, "");

			Assert.That(result, Is.InstanceOf<CountQueryPart>());
			Assert.That(result.ToString(), Is.EqualTo("$count"));
		}
	}
}
