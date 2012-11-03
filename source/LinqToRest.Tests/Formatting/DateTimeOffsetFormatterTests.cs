// ReSharper disable InconsistentNaming

using System;

using LinqToRest.OData.Formatting;
using LinqToRest.OData.Formatting.Impl;

using NUnit.Framework;

namespace LinqToRest.Tests.Formatting
{
	[TestFixture]
	public class DateTimeOffsetFormatterTests
	{
		private ITypeFormatter _formatter;

		[SetUp]
		public void TestSetUp()
		{
			_formatter = new DateTimeOffsetFormatter();
		}

		[Test]
		public void Format_NullType_ThrowsException()
		{
			var value = DateTimeOffset.Now;

			Assert.That(() => _formatter.Format(null, value), Throws.Exception);
		}

		[Test]
		public void Format_NullValue_ThrowsException()
		{
			Assert.That(() => _formatter.Format(typeof (DateTimeOffset), null), Throws.Exception);
		}

		[Test]
		public void Format_DateTimeOffsetValue_FormattedResult()
		{
			var value = DateTimeOffset.Now;
			var result = _formatter.Format(value);

			Assert.That(result, Is.EqualTo(String.Format("datetimeoffset'{0:yyyy-MM-ddTHH:mm:ss.fffK}'", value)));
		}

		[Test]
		public void Format_NullableDateTimeOffsetValue_FormattedResult()
		{
			DateTimeOffset? value = DateTimeOffset.Now;
			var result = _formatter.Format(value);

			Assert.That(result, Is.EqualTo(String.Format("datetimeoffset'{0:yyyy-MM-ddTHH:mm:ss.fffK}'", value)));
		}
	}
}
