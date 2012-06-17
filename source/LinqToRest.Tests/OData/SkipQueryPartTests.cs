// ReSharper disable InconsistentNaming

using System;

using LinqToRest.OData;

using NUnit.Framework;

namespace LinqToRest.Tests.OData
{
	[TestFixture]
	public class SkipQueryPartTests
	{
		[Test]
		public void ToString_NullSkipCount_ReturnsEmptyString()
		{
			var skipQueryPart = new SkipQueryPart(null);

			Assert.That(skipQueryPart.ToString(), Is.EqualTo(String.Empty));
		}

		[Test]
		public void ToString_ZeroSkipCount_ReturnsCorrectString()
		{
			var skipQueryPart = new SkipQueryPart(0);

			Assert.That(skipQueryPart.ToString(), Is.EqualTo("$skip=0"));
		}
	}
}
