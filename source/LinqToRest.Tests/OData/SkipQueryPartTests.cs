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
		public void SkipQueryPart_InheritsFromIEquatable()
		{
			var actual = new SkipQueryPart();

			Assert.That(actual, Is.InstanceOf<IEquatable<SkipQueryPart>>());
		}

		[Test]
		public void Equals_ObjectNull_ReturnsFalse()
		{
			var queryPart = new SkipQueryPart();
			object other = null;

			var actual = queryPart.Equals(other);

			Assert.That(actual, Is.False);
		}

		[Test]
		public void Equals_SkipQueryPartNull_ReturnsFalse()
		{
			var queryPart = new SkipQueryPart();
			SkipQueryPart other = null;

			var actual = queryPart.Equals(other);

			Assert.That(actual, Is.False);
		}

		[Test]
		public void Equals_ObjectOfDifferentType_ReturnsFalse()
		{
			var queryPart = new SkipQueryPart();
			object other = String.Empty;

			var actual = queryPart.Equals(other);

			Assert.That(actual, Is.False);
		}

		[Test]
		public void Equals_ObjectSelf_ReturnsTrue()
		{
			var queryPart = new SkipQueryPart();
			object other = queryPart;

			var actual = queryPart.Equals(other);

			Assert.That(actual, Is.True);
		}

		[Test]
		public void Equals_Self_ReturnsTrue()
		{
			var queryPart = new SkipQueryPart();
			var other = queryPart;

			var actual = queryPart.Equals(other);

			Assert.That(actual, Is.True);
		}

		[Test]
		public void Equals_AnotherDifferentSkipQueryPart_ReturnsFalse()
		{
			var queryPart = new SkipQueryPart();
			var other = new SkipQueryPart(3);

			var actual = queryPart.Equals(other);

			Assert.That(actual, Is.False);
		}

		[Test]
		public void Equals_AnotherEquivalentSkipQueryPart_ReturnsTrue()
		{
			var queryPart = new SkipQueryPart();
			var other = new SkipQueryPart();

			var actual = queryPart.Equals(other);

			Assert.That(actual, Is.True);
		}

		[Test]
		public void Equals_ObjectAnotherEquivalentSkipQueryPart_ReturnsTrue()
		{
			var queryPart = new SkipQueryPart();
			object other = new SkipQueryPart();

			var actual = queryPart.Equals(other);

			Assert.That(actual, Is.True);
		}

		[Test]
		public void GetHashCode_NoParameterConstructorUsed_ReturnsZero()
		{
			var queryPart = new SkipQueryPart();

			var expected = 0;
			var actual = queryPart.GetHashCode();

			Assert.That(actual, Is.EqualTo(expected));
		}

		[Test]
		public void GetHashCode_NullPassedToConstructor_ReturnsZero()
		{
			var queryPart = new SkipQueryPart(null);

			var expected = 0;
			var actual = queryPart.GetHashCode();

			Assert.That(actual, Is.EqualTo(expected));
		}

		[Test]
		public void GetHashCode_NumberPassedIntoConstructor_ReturnsNumber()
		{
			var queryPart = new SkipQueryPart(5);

			var expected = 5;
			var actual = queryPart.GetHashCode();

			Assert.That(actual, Is.EqualTo(expected));
		}

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
