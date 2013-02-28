// ReSharper disable InconsistentNaming

using System;

using LinqToRest.OData;

using NUnit.Framework;

namespace LinqToRest.Tests.OData
{
	[TestFixture]
	public class FormatQueryPartTests
	{
		[Test]
		public void FormatQueryPart_InheritsFromIEquatable()
		{
			var actual = new FormatQueryPart(ODataFormat.Json);

			Assert.That(actual, Is.InstanceOf<IEquatable<FormatQueryPart>>());
		}

		[Test]
		public void Equals_ObjectNull_ReturnsFalse()
		{
			var queryPart = new FormatQueryPart(ODataFormat.Json);
			object other = null;

			var actual = queryPart.Equals(other);

			Assert.That(actual, Is.False);
		}

		[Test]
		public void Equals_FormatQueryPartNull_ReturnsFalse()
		{
			var queryPart = new FormatQueryPart(ODataFormat.Json);
			FormatQueryPart other = null;

			var actual = queryPart.Equals(other);

			Assert.That(actual, Is.False);
		}

		[Test]
		public void Equals_ObjectOfDifferentType_ReturnsFalse()
		{
			var queryPart = new FormatQueryPart(ODataFormat.Json);
			object other = String.Empty;

			var actual = queryPart.Equals(other);

			Assert.That(actual, Is.False);
		}

		[Test]
		public void Equals_ObjectSelf_ReturnsTrue()
		{
			var queryPart = new FormatQueryPart(ODataFormat.Json);
			object other = queryPart;

			var actual = queryPart.Equals(other);

			Assert.That(actual, Is.True);
		}

		[Test]
		public void Equals_Self_ReturnsTrue()
		{
			var queryPart = new FormatQueryPart(ODataFormat.Json);
			var other = queryPart;

			var actual = queryPart.Equals(other);

			Assert.That(actual, Is.True);
		}

		[Test]
		public void Equals_AnotherDifferentFormatQueryPart_ReturnsFalse()
		{
			var queryPart = new FormatQueryPart(ODataFormat.Json);
			var other = new FormatQueryPart(ODataFormat.Xml);

			var actual = queryPart.Equals(other);

			Assert.That(actual, Is.False);
		}

		[Test]
		public void Equals_AnotherEquivalentFormatQueryPart_ReturnsTrue()
		{
			var queryPart = new FormatQueryPart(ODataFormat.Json);
			var other = new FormatQueryPart(ODataFormat.Json);

			var actual = queryPart.Equals(other);

			Assert.That(actual, Is.True);
		}

		[Test]
		public void Equals_ObjectAnotherEquivalentFormatQueryPart_ReturnsTrue()
		{
			var queryPart = new FormatQueryPart(ODataFormat.Json);
			object other = new FormatQueryPart(ODataFormat.Json);

			var actual = queryPart.Equals(other);

			Assert.That(actual, Is.True);
		}

		[Test]
		public void GetHashCode_JsonDataFormat_ReturnsJsonHashCode()
		{
			var queryPart = new FormatQueryPart(ODataFormat.Json);

			var expected = ODataFormat.Json.GetHashCode();
			var actual = queryPart.GetHashCode();

			Assert.That(actual, Is.EqualTo(expected));
		}

		[Test]
		public void GetHashCode_XmlDataFormat_ReturnsXmlHashCode()
		{
			var queryPart = new FormatQueryPart(ODataFormat.Xml);

			var expected = ODataFormat.Xml.GetHashCode();
			var actual = queryPart.GetHashCode();

			Assert.That(actual, Is.EqualTo(expected));
		}

		[Test]
		public void GetHashCode_AtomDataFormat_ReturnsAtomHashCode()
		{
			var queryPart = new FormatQueryPart(ODataFormat.Atom);

			var expected = ODataFormat.Atom.GetHashCode();
			var actual = queryPart.GetHashCode();

			Assert.That(actual, Is.EqualTo(expected));
		}

		[Test]
		public void ToString_Json_ReturnsCorrectString()
		{
			var formatQueryPart = new FormatQueryPart(ODataFormat.Json);

			Assert.That(formatQueryPart.ToString(), Is.EqualTo("$format=json"));
		}

		[Test]
		public void ToString_Xml_ReturnsCorrectString()
		{
			var formatQueryPart = new FormatQueryPart(ODataFormat.Xml);

			Assert.That(formatQueryPart.ToString(), Is.EqualTo("$format=xml"));
		}

		[Test]
		public void ToString_Atom_ReturnsCorrectString()
		{
			var formatQueryPart = new FormatQueryPart(ODataFormat.Atom);

			Assert.That(formatQueryPart.ToString(), Is.EqualTo("$format=atom"));
		}
	}
}
