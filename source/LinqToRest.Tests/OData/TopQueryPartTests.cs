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
		public void TopQueryPart_InheritsFromIEquatable()
		{
			var actual = new TopQueryPart();

			Assert.That(actual, Is.InstanceOf<IEquatable<TopQueryPart>>());
		}

		[Test]
		public void Equals_ObjectNull_ReturnsFalse()
		{
			var queryPart = new TopQueryPart();
			object other = null;

			var actual = queryPart.Equals(other);

			Assert.That(actual, Is.False);
		}

		[Test]
		public void Equals_TopQueryPartNull_ReturnsFalse()
		{
			var queryPart = new TopQueryPart();
			TopQueryPart other = null;

			var actual = queryPart.Equals(other);

			Assert.That(actual, Is.False);
		}

		[Test]
		public void Equals_ObjectOfDifferentType_ReturnsFalse()
		{
			var queryPart = new TopQueryPart();
			object other = String.Empty;

			var actual = queryPart.Equals(other);

			Assert.That(actual, Is.False);
		}

		[Test]
		public void Equals_ObjectSelf_ReturnsTrue()
		{
			var queryPart = new TopQueryPart();
			object other = queryPart;

			var actual = queryPart.Equals(other);

			Assert.That(actual, Is.True);
		}

		[Test]
		public void Equals_Self_ReturnsTrue()
		{
			var queryPart = new TopQueryPart();
			var other = queryPart;

			var actual = queryPart.Equals(other);

			Assert.That(actual, Is.True);
		}

		[Test]
		public void Equals_AnotherDifferentTopQueryPart_ReturnsFalse()
		{
			var queryPart = new TopQueryPart();
			var other = new TopQueryPart(3);

			var actual = queryPart.Equals(other);

			Assert.That(actual, Is.False);
		}

		[Test]
		public void Equals_AnotherEquivalentTopQueryPart_ReturnsTrue()
		{
			var queryPart = new TopQueryPart();
			var other = new TopQueryPart();

			var actual = queryPart.Equals(other);

			Assert.That(actual, Is.True);
		}

		[Test]
		public void Equals_ObjectAnotherEquivalentTopQueryPart_ReturnsTrue()
		{
			var queryPart = new TopQueryPart();
			object other = new TopQueryPart();

			var actual = queryPart.Equals(other);

			Assert.That(actual, Is.True);
		}

		[Test]
		public void GetHashCode_NoParameterConstructorUsed_ReturnsZero()
		{
			var queryPart = new TopQueryPart();

			var expected = 0;
			var actual = queryPart.GetHashCode();

			Assert.That(actual, Is.EqualTo(expected));
		}

		[Test]
		public void GetHashCode_NullPassedToConstructor_ReturnsZero()
		{
			var queryPart = new TopQueryPart(null);

			var expected = 0;
			var actual = queryPart.GetHashCode();

			Assert.That(actual, Is.EqualTo(expected));
		}

		[Test]
		public void GetHashCode_NumberPassedIntoConstructor_ReturnsNumber()
		{
			var queryPart = new TopQueryPart(5);

			var expected = 5;
			var actual = queryPart.GetHashCode();

			Assert.That(actual, Is.EqualTo(expected));
		}

		[Test]
		public void ToString_NullCount_ReturnsEmptyString()
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
