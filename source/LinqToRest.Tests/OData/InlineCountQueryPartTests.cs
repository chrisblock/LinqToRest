// ReSharper disable InconsistentNaming

using LinqToRest.OData;

using NUnit.Framework;

namespace LinqToRest.Tests.OData
{
	[TestFixture]
	public class InlineCountQueryPartTests
	{
		[Test]
		public void ToString_None_ReturnsCorrectString()
		{
			var inlineCountQueryPart = new InlineCountQueryPart(InlineCountType.None);

			Assert.That(inlineCountQueryPart.ToString(), Is.EqualTo("$inlinecount=none"));
		}

		[Test]
		public void ToString_AllPages_ReturnsCorrectString()
		{
			var inlineCountQueryPart = new InlineCountQueryPart(InlineCountType.AllPages);

			Assert.That(inlineCountQueryPart.ToString(), Is.EqualTo("$inlinecount=allpages"));
		}
	}
}
