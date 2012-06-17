// ReSharper disable InconsistentNaming

using System;

using LinqToRest.OData;
using LinqToRest.OData.Filters;

using NUnit.Framework;

namespace LinqToRest.Tests.OData
{
	[TestFixture]
	public class SelectQueryPartTests
	{
		[Test]
		public void ToString_NoMembers_ReturnsEmptyString()
		{
			var selectQueryPart = new SelectQueryPart();

			Assert.That(selectQueryPart.ToString(), Is.EqualTo(String.Empty));
		}

		[Test]
		public void ToString_TwoMembers_ReturnsCommaSeperatedString()
		{
			var selectQueryPart = new SelectQueryPart(FilterExpression.MemberAccess("TestString"), FilterExpression.MemberAccess("TestInt"));

			Assert.That(selectQueryPart.ToString(), Is.EqualTo("$select=TestString, TestInt"));
		}
	}
}
