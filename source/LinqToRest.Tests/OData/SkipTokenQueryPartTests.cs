// ReSharper disable InconsistentNaming

using System;

using LinqToRest.OData;

using NUnit.Framework;

namespace LinqToRest.Tests.OData
{
	[TestFixture]
	public class SkipTokenQueryPartTests
	{
		[Test]
		public void ToString_NullPredicate_ReturnsEmptyString()
		{
			var skipTokenQueryPart = new SkipTokenQueryPart(null);

			Assert.That(skipTokenQueryPart.ToString(), Is.EqualTo(String.Empty));
		}
	}
}
