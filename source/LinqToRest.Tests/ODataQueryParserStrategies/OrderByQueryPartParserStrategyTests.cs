using System.Linq;

using LinqToRest.OData;
using LinqToRest.OData.Parsing;
using LinqToRest.OData.Parsing.Impl;

using NUnit.Framework;

namespace LinqToRest.Tests.ODataQueryParserStrategies
{
	[TestFixture]
	public class OrderByQueryPartParserStrategyTests
	{
		private const ODataQueryPartType Type = ODataQueryPartType.OrderBy;
		private readonly IODataQueryParserStrategy _strategy = new OrderByQueryPartParserStrategy();

		[Test]
		public void Parse_IncorrectType_ThrowsArgumentException()
		{
			Assert.That(() => _strategy.Parse(ODataQueryPartType.Complete, ""), Throws.ArgumentException);
		}

		[Test]
		public void Parse_StringContainingValidOrderByPredicate_ReturnsCorrectQueryPart()
		{
			var result = _strategy.Parse(Type, "TestProperty asc, TestDate desc");

			Assert.That(result, Is.InstanceOf<ODataOrderByQueryPart>());
			Assert.That(((ODataOrderByQueryPart)result).Orderings.Count, Is.EqualTo(2));
			Assert.That(((ODataOrderByQueryPart)result).Orderings.ElementAt(0).Field, Is.EqualTo("TestProperty"));
			Assert.That(((ODataOrderByQueryPart)result).Orderings.ElementAt(0).Direction, Is.EqualTo(ODataOrderingDirection.Asc));
			Assert.That(((ODataOrderByQueryPart)result).Orderings.ElementAt(1).Field, Is.EqualTo("TestDate"));
			Assert.That(((ODataOrderByQueryPart)result).Orderings.ElementAt(1).Direction, Is.EqualTo(ODataOrderingDirection.Desc));
			Assert.That(result.ToString(), Is.EqualTo("$orderby=TestProperty asc, TestDate desc"));
		}
	}
}
