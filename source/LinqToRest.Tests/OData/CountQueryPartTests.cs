// ReSharper disable InconsistentNaming

using System;

using LinqToRest.OData;

using NUnit.Framework;

namespace LinqToRest.Tests.OData
{
	[TestFixture]
	public class CountQueryPartTests
	{
		[Test]
		public void CountQueryPart_InheritsFromIEquatable()
		{
			var actual = new CountQueryPart();

			Assert.That(actual, Is.InstanceOf<IEquatable<CountQueryPart>>());
		}

		[Test]
		public void Equals_ObjectNull_ReturnsFalse()
		{
			var queryPart = new CountQueryPart();
			object other = null;

			var expected = false;
			var actual = queryPart.Equals(other);

			Assert.That(actual, Is.EqualTo(expected));
		}

		[Test]
		public void Equals_ObjectOfDifferentType_ReturnsFalse()
		{
			var queryPart = new CountQueryPart();
			string other = String.Empty;

			var expected = false;
			var actual = queryPart.Equals(other);

			Assert.That(actual, Is.EqualTo(expected));
		}

		[Test]
		public void Equals_ObjectSelf_ReturnsTrue()
		{
			var queryPart = new CountQueryPart();
			object other = queryPart;

			var expected = true;
			var actual = queryPart.Equals(other);

			Assert.That(actual, Is.EqualTo(expected));
		}

		[Test]
		public void Equals_Self_ReturnsTrue()
		{
			var queryPart = new CountQueryPart();
			var other = queryPart;

			var expected = true;
			var actual = queryPart.Equals(other);

			Assert.That(actual, Is.EqualTo(expected));
		}

		[Test]
		public void Equals_AnotherEquivalentCountQueryPart_ReturnsTrue()
		{
			var queryPart = new CountQueryPart();
			var other = new CountQueryPart();

			var expected = true;
			var actual = queryPart.Equals(other);

			Assert.That(actual, Is.EqualTo(expected));
		}

		[Test]
		public void Equals_ObjectAnotherEquivalentCountQueryPart_ReturnsTrue()
		{
			var queryPart = new CountQueryPart();
			object other = new CountQueryPart();

			var expected = true;
			var actual = queryPart.Equals(other);

			Assert.That(actual, Is.EqualTo(expected));
		}

		[Test]
		public void GetHashCode_CountQueryPart_ReturnsZero()
		{
			var queryPart = new CountQueryPart();

			var expected = 0;
			var actual = queryPart.GetHashCode();

			Assert.That(actual, Is.EqualTo(expected));
		}

		[Test]
		public void ToString_NoParameters_ReturnsCorrectString()
		{
			var countQueryPart = new CountQueryPart();

			Assert.That(countQueryPart.ToString(), Is.EqualTo("$count"));
		}
	}
}
