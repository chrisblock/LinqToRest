// ReSharper disable InconsistentNaming

using System;

using LinqToRest.OData;

using NUnit.Framework;

namespace LinqToRest.Tests.OData
{
	[TestFixture]
	public class TopQueryPartTests
	{
		[Test]
		public void ToString_NullTakeCount_ReturnsEmptyString()
		{
			var skipQueryPart = new TopQueryPart(null);

			Assert.That(skipQueryPart.ToString(), Is.EqualTo(String.Empty));
		}

		[Test]
		public void ToString_ZeroTakeCount_ReturnsCorrectString()
		{
			var skipQueryPart = new TopQueryPart(0);

			Assert.That(skipQueryPart.ToString(), Is.EqualTo("$top=0"));
		}
	}
}
