// ReSharper disable InconsistentNaming

using LinqToRest.OData.Filters;

using NUnit.Framework;

namespace LinqToRest.Tests.Filters
{
	[TestFixture]
	public class MemberAccessFilterExpressionTests
	{
		[Test]
		public void ToString_NoInstanceExpression_ReturnsCorrectString()
		{
			var memberExpression = new MemberAccessFilterExpression(null, "TestString");

			Assert.That(memberExpression.ToString(), Is.EqualTo("TestString"));
		}

		[Test]
		public void ToString_TwoLevelInstanceExpressions_ReturnsCorrectString()
		{
			var memberExpression = new MemberAccessFilterExpression(new MemberAccessFilterExpression(new MemberAccessFilterExpression(null, "TestChild1"), "TestChild2"), "TestString");

			Assert.That(memberExpression.ToString(), Is.EqualTo("TestChild1/TestChild2/TestString"));
		}
	}
}
