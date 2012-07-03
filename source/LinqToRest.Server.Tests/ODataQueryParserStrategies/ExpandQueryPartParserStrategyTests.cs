// ReSharper disable InconsistentNaming

using System.Linq;

using LinqToRest.OData;
using LinqToRest.OData.Filters;
using LinqToRest.Server.OData.Parsing;
using LinqToRest.Server.OData.Parsing.Impl;

using NUnit.Framework;

namespace LinqToRest.Server.Tests.ODataQueryParserStrategies
{
	[TestFixture]
	public class ExpandQueryPartParserStrategyTests
	{
		private const ODataQueryPartType Type = ODataQueryPartType.Expand;
		private readonly IODataQueryPartParserStrategy _strategy = new ExpandQueryPartParserStrategy();

		[Test]
		public void Parse_IncorrectType_ThrowsArgumentException()
		{
			Assert.That(() => _strategy.Parse(ODataQueryPartType.Ordering, ""), Throws.ArgumentException);
		}

		[Test]
		public void Parse_StringContainingIntegerTen_ReturnsCorrectQueryPart()
		{
			var result = _strategy.Parse(Type, "TestProperty");

			Assert.That(result, Is.InstanceOf<ExpandQueryPart>());
			Assert.That(((ExpandQueryPart)result).Members, Is.Not.Null);
			Assert.That(((ExpandQueryPart)result).Members.First(), Is.EqualTo(FilterExpression.MemberAccess("TestProperty")));
			Assert.That(result.ToString(), Is.EqualTo("$expand=TestProperty"));
		}
	}
}
