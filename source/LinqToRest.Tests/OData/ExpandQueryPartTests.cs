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
		public void ExpandQueryPart_InheritsFromIEquatable()
		{
			var actual = new ExpandQueryPart();

			Assert.That(actual, Is.InstanceOf<IEquatable<ExpandQueryPart>>());
		}

		[Test]
		public void Equals_ObjectNull_ReturnsFalse()
		{
			var queryPart = new ExpandQueryPart();
			object other = null;

			var actual = queryPart.Equals(other);

			Assert.That(actual, Is.False);
		}

		[Test]
		public void Equals_ExpandQueryPartNull_ReturnsFalse()
		{
			var queryPart = new ExpandQueryPart();
			ExpandQueryPart other = null;

			var actual = queryPart.Equals(other);

			Assert.That(actual, Is.False);
		}

		[Test]
		public void Equals_ObjectOfDifferentType_ReturnsFalse()
		{
			var queryPart = new ExpandQueryPart();
			object other = String.Empty;

			var actual = queryPart.Equals(other);

			Assert.That(actual, Is.False);
		}

		[Test]
		public void Equals_ObjectSelf_ReturnsTrue()
		{
			var queryPart = new ExpandQueryPart();
			object other = queryPart;

			var actual = queryPart.Equals(other);

			Assert.That(actual, Is.True);
		}

		[Test]
		public void Equals_Self_ReturnsTrue()
		{
			var queryPart = new ExpandQueryPart();
			var other = queryPart;

			var actual = queryPart.Equals(other);

			Assert.That(actual, Is.True);
		}

		[Test]
		public void Equals_AnotherDifferentExpandQueryPart_ReturnsFalse()
		{
			var queryPart = new ExpandQueryPart(FilterExpression.MemberAccess("Hello"));
			var other = new ExpandQueryPart(FilterExpression.MemberAccess("World"));

			var actual = queryPart.Equals(other);

			Assert.That(actual, Is.False);
		}

		[Test]
		public void Equals_AnotherEquivalentExpandQueryPart_ReturnsTrue()
		{
			var queryPart = new ExpandQueryPart(FilterExpression.MemberAccess("Hello"));
			var other = new ExpandQueryPart(FilterExpression.MemberAccess("Hello"));

			var actual = queryPart.Equals(other);

			Assert.That(actual, Is.True);
		}

		[Test]
		public void Equals_ObjectAnotherEquivalentExpandQueryPart_ReturnsTrue()
		{
			var queryPart = new ExpandQueryPart();
			object other = new ExpandQueryPart();

			var actual = queryPart.Equals(other);

			Assert.That(actual, Is.True);
		}

		[Test]
		public void GetHashCode_NoMembers_ReturnsZero()
		{
			var queryPart = new ExpandQueryPart();

			var expected = 0;
			var actual = queryPart.GetHashCode();

			Assert.That(actual, Is.EqualTo(expected));
		}

		[Test]
		public void GetHashCode_TwoMembers_ReturnsCorrectResult()
		{
			var queryPart = new ExpandQueryPart(FilterExpression.MemberAccess("Hello"), FilterExpression.MemberAccess("World"));

			var expected = "Members:Hello,World;".GetHashCode();
			var actual = queryPart.GetHashCode();

			Assert.That(actual, Is.EqualTo(expected));
		}

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
