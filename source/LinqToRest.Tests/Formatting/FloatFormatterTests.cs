// ReSharper disable InconsistentNaming

using LinqToRest.OData.Formatting;
using LinqToRest.OData.Formatting.Impl;

using NUnit.Framework;

namespace LinqToRest.Tests.Formatting
{
	[TestFixture]
	public class FloatFormatterTests
	{
		private ITypeFormatter _formatter;

		[SetUp]
		public void TestSetUp()
		{
			_formatter = new FloatFormatter();
		}

		[Test]
		public void Format_NullType_ThrowsException()
		{
			var value = 42f;

			Assert.That(() => _formatter.Format(null, value), Throws.Exception);
		}

		[Test]
		public void Format_NullValue_ThrowsException()
		{
			Assert.That(() => _formatter.Format(typeof(float), null), Throws.Exception);
		}

		[Test]
		public void Format_FloatValue_FormattedResult()
		{
			var value = 42f;
			var result = _formatter.Format(value);

			Assert.That(result, Is.EqualTo("42f"));
		}
	}
}
