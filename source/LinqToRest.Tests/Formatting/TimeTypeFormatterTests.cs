// ReSharper disable InconsistentNaming

using System;

using LinqToRest.OData.Formatting;
using LinqToRest.OData.Formatting.Impl;

using NUnit.Framework;

namespace LinqToRest.Tests.Formatting
{
	[TestFixture]
	public class TimeTypeFormatterTests
	{
		private ITypeFormatter _formatter;

		[SetUp]
		public void TestSetUp()
		{
			_formatter = new TimeFormatter();
		}

		[Test]
		public void Format_NullType_ThrowsException()
		{
			var value = DateTime.Now.TimeOfDay;

			Assert.That(() => _formatter.Format(null, value), Throws.Exception);
		}

		[Test]
		public void Format_NullValue_ThrowsException()
		{
			Assert.That(() => _formatter.Format(typeof (TimeSpan), null), Throws.Exception);
		}

		[Test]
		public void Format_TimeValue_FormattedResult()
		{
			var value = DateTime.Now.TimeOfDay;
			var result = _formatter.Format(value);

			Assert.That(result, Is.EqualTo(String.Format("time'{0:'P0Y0M'd'DT'h'H'm'M's'.'ffffff'S'}'", value)));
		}

		[Test]
		public void Format_NullableTimeValue_FormattedResult()
		{
			TimeSpan? value = DateTime.Now.TimeOfDay;
			var result = _formatter.Format(value);

			Assert.That(result, Is.EqualTo(String.Format("time'{0:'P0Y0M'd'DT'h'H'm'M's'.'ffffff'S'}'", value)));
		}
	}
}
