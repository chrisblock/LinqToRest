// ReSharper disable InconsistentNaming

using System;

using LinqToRest.OData;

using NUnit.Framework;

namespace LinqToRest.Tests.OData
{
	[TestFixture]
	public class InlineCountQueryPartTests
	{
		[Test]
		public void InlineCountQueryPart_InheritsFromIEquatable()
		{
			var actual = new InlineCountQueryPart(InlineCountType.None);

			Assert.That(actual, Is.InstanceOf<IEquatable<InlineCountQueryPart>>());
		}

		[Test]
		public void Equals_ObjectNull_ReturnsFalse()
		{
			var queryPart = new InlineCountQueryPart(InlineCountType.None);
			object other = null;

			var actual = queryPart.Equals(other);

			Assert.That(actual, Is.False);
		}

		[Test]
		public void Equals_InlineCountQueryPartNull_ReturnsFalse()
		{
			var queryPart = new InlineCountQueryPart(InlineCountType.None);
			InlineCountQueryPart other = null;

			var actual = queryPart.Equals(other);

			Assert.That(actual, Is.False);
		}

		[Test]
		public void Equals_ObjectOfDifferentType_ReturnsFalse()
		{
			var queryPart = new InlineCountQueryPart(InlineCountType.None);
			object other = String.Empty;

			var actual = queryPart.Equals(other);

			Assert.That(actual, Is.False);
		}

		[Test]
		public void Equals_ObjectSelf_ReturnsTrue()
		{
			var queryPart = new InlineCountQueryPart(InlineCountType.None);
			object other = queryPart;

			var actual = queryPart.Equals(other);

			Assert.That(actual, Is.True);
		}

		[Test]
		public void Equals_Self_ReturnsTrue()
		{
			var queryPart = new InlineCountQueryPart(InlineCountType.None);
			var other = queryPart;

			var actual = queryPart.Equals(other);

			Assert.That(actual, Is.True);
		}

		[Test]
		public void Equals_AnotherDifferentInlineCountQueryPart_ReturnsFalse()
		{
			var queryPart = new InlineCountQueryPart(InlineCountType.None);
			var other = new InlineCountQueryPart(InlineCountType.AllPages);

			var actual = queryPart.Equals(other);

			Assert.That(actual, Is.False);
		}

		[Test]
		public void Equals_AnotherEquivalentInlineCountQueryPart_ReturnsTrue()
		{
			var queryPart = new InlineCountQueryPart(InlineCountType.None);
			var other = new InlineCountQueryPart(InlineCountType.None);

			var actual = queryPart.Equals(other);

			Assert.That(actual, Is.True);
		}

		[Test]
		public void Equals_ObjectAnotherEquivalentInlineCountQueryPart_ReturnsTrue()
		{
			var queryPart = new InlineCountQueryPart(InlineCountType.None);
			object other = new InlineCountQueryPart(InlineCountType.None);

			var actual = queryPart.Equals(other);

			Assert.That(actual, Is.True);
		}

		[Test]
		public void GetHashCode_InlineCountNone_ReturnsInlineCountNoneHashCode()
		{
			var queryPart = new InlineCountQueryPart(InlineCountType.None);

			var expected = InlineCountType.None.GetHashCode();
			var actual = queryPart.GetHashCode();

			Assert.That(actual, Is.EqualTo(expected));
		}

		[Test]
		public void GetHashCode_InlineCountAllPages_ReturnsInlineCountAllPagesHashCode()
		{
			var queryPart = new InlineCountQueryPart(InlineCountType.AllPages);

			var expected = InlineCountType.AllPages.GetHashCode();
			var actual = queryPart.GetHashCode();

			Assert.That(actual, Is.EqualTo(expected));
		}

		[Test]
		public void ToString_None_ReturnsCorrectString()
		{
			var inlineCountQueryPart = new InlineCountQueryPart(InlineCountType.None);

			Assert.That(inlineCountQueryPart.ToString(), Is.EqualTo("$inlinecount=none"));
		}

		[Test]
		public void ToString_AllPages_ReturnsCorrectString()
		{
			var inlineCountQueryPart = new InlineCountQueryPart(InlineCountType.AllPages);

			Assert.That(inlineCountQueryPart.ToString(), Is.EqualTo("$inlinecount=allpages"));
		}
	}
}
