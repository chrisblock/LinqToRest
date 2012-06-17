// ReSharper disable InconsistentNaming

using System;

using LinqToRest.OData;
using LinqToRest.OData.Filters;

using NUnit.Framework;

namespace LinqToRest.Tests.OData
{
	[TestFixture]
	public class ExpandQueryPartTests
	{
		[Test]
		public void ToString_NoMembers_EmptyString()
		{
			var expandQueryPart = new ExpandQueryPart();

			Assert.That(expandQueryPart.ToString(), Is.EqualTo(String.Empty));
		}

		[Test]
		public void ToString_TwoMembers_EmptyString()
		{
			var expandQueryPart = new ExpandQueryPart(FilterExpression.MemberAccess("TestString"), FilterExpression.MemberAccess("TestInt"));

			Assert.That(expandQueryPart.ToString(), Is.EqualTo("$expand=TestString, TestInt"));
		}
	}
}
