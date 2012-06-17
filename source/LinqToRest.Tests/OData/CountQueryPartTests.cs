// ReSharper disable InconsistentNaming

using LinqToRest.OData;

using NUnit.Framework;

namespace LinqToRest.Tests.OData
{
	[TestFixture]
	public class CountQueryPartTests
	{
		[Test]
		public void ToString_NoParameters_ReturnsCorrectString()
		{
			var countQueryPart = new CountQueryPart();

			Assert.That(countQueryPart.ToString(), Is.EqualTo("$count"));
		}
	}
}
