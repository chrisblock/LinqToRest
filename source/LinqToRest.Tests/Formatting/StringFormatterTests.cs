// ReSharper disable InconsistentNaming

using LinqToRest.OData.Formatting;
using LinqToRest.OData.Formatting.Impl;

using NUnit.Framework;

namespace LinqToRest.Tests.Formatting
{
	[TestFixture]
	public class StringFormatterTests
	{
		private ITypeFormatter _formatter;

		[SetUp]
		public void TestSetUp()
		{
			_formatter = new StringFormatter();
		}

		[Test]
		public void Format_NullType_ThrowsException()
		{
			var value = 42L;

			Assert.That(() => _formatter.Format(null, value), Throws.Exception);
		}

		[Test]
		public void Format_NullValue_ThrowsException()
		{
			Assert.That(() => _formatter.Format(typeof (string), null), Throws.Exception);
		}

		[Test]
		public void Format_StringValue_FormattedResult()
		{
			var value = "Hello World.";
			var result = _formatter.Format(value);

			Assert.That(result, Is.EqualTo("'Hello World.'"));
		}
	}
}
