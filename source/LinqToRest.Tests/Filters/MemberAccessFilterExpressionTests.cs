// ReSharper disable InconsistentNaming

using System;

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

		[Test]
		public void GetHashCode_ReturnsCorrectValue()
		{
			var memberExpression = new MemberAccessFilterExpression(new MemberAccessFilterExpression(null, "TestObject"), "TestString");

			Assert.That(memberExpression.GetHashCode(), Is.EqualTo(String.Format("Instance:TestObject;Member:TestString;").GetHashCode()));
		}

		[Test]
		public void ObjectEquals_Null_ReturnsFalse()
		{
			var memberExpression = new MemberAccessFilterExpression(null, "TestString");

			object other = null;

			Assert.That(memberExpression.Equals(other), Is.False);
		}

		[Test]
		public void ObjectEquals_SameReference_ReturnsTrue()
		{
			var memberExpression = new MemberAccessFilterExpression(null, "TestString");

			object other = memberExpression;

			Assert.That(memberExpression.Equals(other), Is.True);
		}

		[Test]
		public void ObjectEquals_NonMemberAccessFilterExpressionObject_ReturnsFalse()
		{
			var memberExpression = new MemberAccessFilterExpression(null, "TestString");

			object other = "Hello World.";

			Assert.That(memberExpression.Equals(other), Is.False);
		}

		[Test]
		public void ObjectEquals_EqualInstanceAndMember_ReturnsTrue()
		{
			var memberExpression = new MemberAccessFilterExpression(null, "TestString");

			object other = new MemberAccessFilterExpression(null, "TestString");

			Assert.That(memberExpression.Equals(other), Is.True);
		}

		[Test]
		public void ObjectEquals_EqualInstanceAndNotEqualMember_ReturnsFalse()
		{
			var memberExpression = new MemberAccessFilterExpression(null, "TestString");

			object other = new MemberAccessFilterExpression(null, "TestInteger");

			Assert.That(memberExpression.Equals(other), Is.False);
		}

		[Test]
		public void ObjectEquals_EqualNotEqualInstanceAndMember_ReturnsFalse()
		{
			var memberExpression = new MemberAccessFilterExpression(null, "TestString");

			object other = new MemberAccessFilterExpression(new MemberAccessFilterExpression(null, "TestObject"), "TestString");

			Assert.That(memberExpression.Equals(other), Is.False);
		}

		[Test]
		public void Equals_Null_ReturnsFalse()
		{
			var memberExpression = new MemberAccessFilterExpression(null, "TestString");

			MemberAccessFilterExpression other = null;

			Assert.That(memberExpression.Equals(other), Is.False);
		}

		[Test]
		public void Equals_SameReference_ReturnsTrue()
		{
			var memberExpression = new MemberAccessFilterExpression(null, "TestString");

			MemberAccessFilterExpression other = memberExpression;

			Assert.That(memberExpression.Equals(other), Is.True);
		}

		[Test]
		public void Equals_EqualInstanceAndMember_ReturnsTrue()
		{
			var memberExpression = new MemberAccessFilterExpression(null, "TestString");

			var other = new MemberAccessFilterExpression(null, "TestString");

			Assert.That(memberExpression.Equals(other), Is.True);
		}

		[Test]
		public void Equals_EqualInstanceAndNotEqualMember_ReturnsFalse()
		{
			var memberExpression = new MemberAccessFilterExpression(null, "TestString");

			var other = new MemberAccessFilterExpression(null, "TestInteger");

			Assert.That(memberExpression.Equals(other), Is.False);
		}

		[Test]
		public void Equals_EqualNotEqualInstanceAndMember_ReturnsFalse()
		{
			var memberExpression = new MemberAccessFilterExpression(null, "TestString");

			var other = new MemberAccessFilterExpression(new MemberAccessFilterExpression(null, "TestObject"), "TestString");

			Assert.That(memberExpression.Equals(other), Is.False);
		}
	}
}
