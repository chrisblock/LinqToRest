// ReSharper disable InconsistentNaming

using LinqToRest.OData.Formatting;
using LinqToRest.OData.Formatting.Impl;

using NUnit.Framework;

namespace LinqToRest.Tests.Formatting
{
	[TestFixture]
	public class DecimalFormatterTests
	{
		private ITypeFormatter _formatter;

		[SetUp]
		public void TestSetUp()
		{
			_formatter = new DecimalFormatter();
		}

		[Test]
		public void Format_NullType_ThrowsException()
		{
			var value = 42m;

			Assert.That(() => _formatter.Format(null, value), Throws.Exception);
		}

		[Test]
		public void Format_NullValue_ThrowsException()
		{
			Assert.That(() => _formatter.Format(typeof (decimal), null), Throws.Exception);
		}

		[Test]
		public void Format_DecimalValue_FormattedResult()
		{
			var value = 42m;
			var result = _formatter.Format(value);

			Assert.That(result, Is.EqualTo("42m"));
		}
	}
}
