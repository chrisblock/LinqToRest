// ReSharper disable InconsistentNaming

using System;

using LinqToRest.OData;

using NUnit.Framework;

namespace LinqToRest.Tests.OData
{
	[TestFixture]
	public class ODataOrderingTests
	{
		[Test]
		public void Constructor_NullAscending_ThrowsArgumentException()
		{
			Assert.That(() => new ODataOrdering(null, ODataOrderingDirection.Asc), Throws.ArgumentException);
		}

		[Test]
		public void Constructor_WhiteSpaceAscending_ThrowsArgumentException()
		{
			Assert.That(() => new ODataOrdering("  \t\r\n", ODataOrderingDirection.Asc), Throws.ArgumentException);
		}

		[Test]
		public void ODataOrdering_InheritsFromIEquatable()
		{
			var actual = new ODataOrdering("Hello", ODataOrderingDirection.Asc);

			Assert.That(actual, Is.InstanceOf<IEquatable<ODataOrdering>>());
		}

		[Test]
		public void Equals_ObjectNull_ReturnsFalse()
		{
			var queryPart = new ODataOrdering("Hello", ODataOrderingDirection.Asc);
			object other = null;

			var actual = queryPart.Equals(other);

			Assert.That(actual, Is.False);
		}

		[Test]
		public void Equals_ODataOrderingNull_ReturnsFalse()
		{
			var queryPart = new ODataOrdering("Hello", ODataOrderingDirection.Asc);
			ODataOrdering other = null;

			var actual = queryPart.Equals(other);

			Assert.That(actual, Is.False);
		}

		[Test]
		public void Equals_ObjectOfDifferentType_ReturnsFalse()
		{
			var queryPart = new ODataOrdering("Hello", ODataOrderingDirection.Asc);
			object other = String.Empty;

			var actual = queryPart.Equals(other);

			Assert.That(actual, Is.False);
		}

		[Test]
		public void Equals_ObjectSelf_ReturnsTrue()
		{
			var queryPart = new ODataOrdering("Hello", ODataOrderingDirection.Asc);
			object other = queryPart;

			var actual = queryPart.Equals(other);

			Assert.That(actual, Is.True);
		}

		[Test]
		public void Equals_Self_ReturnsTrue()
		{
			var queryPart = new ODataOrdering("Hello", ODataOrderingDirection.Asc);
			var other = queryPart;

			var actual = queryPart.Equals(other);

			Assert.That(actual, Is.True);
		}

		[Test]
		public void Equals_AnotherODataOrderingDifferentDirection_ReturnsFalse()
		{
			var queryPart = new ODataOrdering("Hello", ODataOrderingDirection.Asc);
			var other = new ODataOrdering("Hello", ODataOrderingDirection.Desc);

			var actual = queryPart.Equals(other);

			Assert.That(actual, Is.False);
		}

		[Test]
		public void Equals_AnotherODataOrderingDifferentFieldName_ReturnsFalse()
		{
			var queryPart = new ODataOrdering("Hello", ODataOrderingDirection.Asc);
			var other = new ODataOrdering("World", ODataOrderingDirection.Asc);

			var actual = queryPart.Equals(other);

			Assert.That(actual, Is.False);
		}

		[Test]
		public void Equals_AnotherEquivalentODataOrdering_ReturnsTrue()
		{
			var queryPart = new ODataOrdering("Hello", ODataOrderingDirection.Asc);
			var other = new ODataOrdering("Hello", ODataOrderingDirection.Asc);

			var actual = queryPart.Equals(other);

			Assert.That(actual, Is.True);
		}

		[Test]
		public void Equals_ObjectAnotherEquivalentODataOrdering_ReturnsTrue()
		{
			var queryPart = new ODataOrdering("Hello", ODataOrderingDirection.Asc);
			object other = new ODataOrdering("Hello", ODataOrderingDirection.Asc);

			var actual = queryPart.Equals(other);

			Assert.That(actual, Is.True);
		}

		[Test]
		public void GetHashCode_FieldAndDirection_ReturnsCorrectValue()
		{
			var queryPart = new ODataOrdering("Hello", ODataOrderingDirection.Asc);

			var expected = String.Format("Field:Hello;Direction:Asc;");

			Assert.That(queryPart.GetHashCode(), Is.EqualTo(expected.GetHashCode()));
		}

		[Test]
		public void QueryPartType_ReturnsOrdering()
		{
			var ordering = new ODataOrdering("ColumnName", ODataOrderingDirection.Asc);

			Assert.That(ordering.QueryPartType, Is.EqualTo(ODataQueryPartType.Ordering));
		}

		[Test]
		public void ToString_ColumnNameAscending_ReturnsCorrectString()
		{
			var ordering = new ODataOrdering("ColumnName", ODataOrderingDirection.Asc);

			Assert.That(ordering.ToString(), Is.EqualTo("ColumnName asc"));
		}

		[Test]
		public void ToString_ColumnNameDescending_ReturnsCorrectString()
		{
			var ordering = new ODataOrdering("ColumnName", ODataOrderingDirection.Desc);

			Assert.That(ordering.ToString(), Is.EqualTo("ColumnName desc"));
		}
	}
}
