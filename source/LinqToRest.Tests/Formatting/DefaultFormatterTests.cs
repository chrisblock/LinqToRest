// ReSharper disable InconsistentNaming

using LinqToRest.OData.Formatting;
using LinqToRest.OData.Formatting.Impl;

using NUnit.Framework;

namespace LinqToRest.Tests.Formatting
{
	[TestFixture]
	public class DefaultFormatterTests
	{
		private ITypeFormatter _formatter;

		[SetUp]
		public void TestSetUp()
		{
			_formatter = new DefaultFormatter();
		}

		[Test]
		public void Format_NullType_ThrowsException()
		{
			var value = "HelloWorld";

			Assert.That(() => _formatter.Format(null, value), Throws.Exception);
		}

		[Test]
		public void Format_NullValue_ThrowsException()
		{
			Assert.That(() => _formatter.Format(typeof (string), null), Throws.Exception);
		}

		[Test]
		public void Format_Value_FormattedResult()
		{
			var value = "HelloWorld";
			var result = _formatter.Format(value);

			Assert.That(result, Is.EqualTo(value));
		}
	}
}
