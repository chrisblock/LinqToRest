// ReSharper disable InconsistentNaming

using LinqToRest.OData;

using NUnit.Framework;

namespace LinqToRest.Tests.OData
{
	[TestFixture]
	public class FormatQueryPartTests
	{
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
