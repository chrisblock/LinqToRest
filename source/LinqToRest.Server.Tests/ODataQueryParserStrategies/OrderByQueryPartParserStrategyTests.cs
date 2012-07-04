// ReSharper disable InconsistentNaming

using System.Linq;

using LinqToRest.OData;
using LinqToRest.Server.OData.Parsing;
using LinqToRest.Server.OData.Parsing.Impl;

using NUnit.Framework;

namespace LinqToRest.Server.Tests.ODataQueryParserStrategies
{
	[TestFixture]
	public class OrderByQueryPartParserStrategyTests
	{
		private const ODataQueryPartType Type = ODataQueryPartType.OrderBy;
		private readonly IODataQueryPartParserStrategy _strategy = new OrderByQueryPartParserStrategy();

		[Test]
		public void Parse_IncorrectType_ThrowsArgumentException()
		{
			Assert.That(() => _strategy.Parse(ODataQueryPartType.Ordering, ""), Throws.ArgumentException);
		}

		[Test]
		public void Parse_StringContainingValidOrderByPredicate_ReturnsCorrectQueryPart()
		{
			var result = _strategy.Parse(Type, "TestProperty asc, TestDate desc");

			Assert.That(result, Is.InstanceOf<OrderByQueryPart>());
			Assert.That(((OrderByQueryPart)result).Orderings.Count, Is.EqualTo(2));
			Assert.That(((OrderByQueryPart)result).Orderings.ElementAt(0).Field, Is.EqualTo("TestProperty"));
			Assert.That(((OrderByQueryPart)result).Orderings.ElementAt(0).Direction, Is.EqualTo(ODataOrderingDirection.Asc));
			Assert.That(((OrderByQueryPart)result).Orderings.ElementAt(1).Field, Is.EqualTo("TestDate"));
			Assert.That(((OrderByQueryPart)result).Orderings.ElementAt(1).Direction, Is.EqualTo(ODataOrderingDirection.Desc));
			Assert.That(result.ToString(), Is.EqualTo("$orderby=TestProperty asc, TestDate desc"));
		}
	}
}
