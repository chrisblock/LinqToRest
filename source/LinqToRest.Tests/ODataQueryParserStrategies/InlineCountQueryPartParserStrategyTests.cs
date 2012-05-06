using LinqToRest.OData;
using LinqToRest.OData.Parsing;
using LinqToRest.OData.Parsing.Impl;

using NUnit.Framework;

namespace LinqToRest.Tests.ODataQueryParserStrategies
{
	[TestFixture]
	public class InlineCountQueryPartParserStrategyTests
	{
		private const ODataQueryPartType Type = ODataQueryPartType.InlineCount;
		private readonly IODataQueryParserStrategy _strategy = new InlineCountQueryPartParserStrategy();

		[Test]
		public void Parse_IncorrectType_ThrowsArgumentException()
		{
			Assert.That(() => _strategy.Parse(ODataQueryPartType.Ordering, "allpages"), Throws.ArgumentException);
		}

		[Test]
		public void Parse_InvalidInlineCountValue_ThrowsArgumentException()
		{
			Assert.That(() => _strategy.Parse(Type, "wait! look over there!"), Throws.ArgumentException);
		}

		[Test]
		public void Parse_AllPagesInlineCountValue_ReturnsCorrectQueryPart()
		{
			var result = _strategy.Parse(Type, "allpages");

			Assert.That(result, Is.InstanceOf<InlineCountQueryPart>());
			Assert.That(((InlineCountQueryPart)result).InlineCountType, Is.EqualTo(InlineCountType.AllPages));
			Assert.That(result.ToString(), Is.EqualTo("$inlinecount=allpages"));
		}

		[Test]
		public void Parse_NoneInlineCountValue_ReturnsCorrectQueryPart()
		{
			var result = _strategy.Parse(Type, "none");

			Assert.That(result, Is.InstanceOf<InlineCountQueryPart>());
			Assert.That(((InlineCountQueryPart)result).InlineCountType, Is.EqualTo(InlineCountType.None));
			Assert.That(result.ToString(), Is.EqualTo("$inlinecount=none"));
		}
	}
}
