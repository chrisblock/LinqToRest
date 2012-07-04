// ReSharper disable InconsistentNaming

using System;

using LinqToRest.OData.Filters;

using NUnit.Framework;

namespace LinqToRest.Tests.Filters
{
	[TestFixture]
	public class ConstantFilterExpressionTests
	{
		private static ConstantFilterExpression CreateConstantExpression<T>(T value)
		{
			return new ConstantFilterExpression(value, typeof (T));
		}

		[Test]
		public void Constructor_NonNullValueArgumentNoTypeArgument_TypeIsInferred()
		{
			var constantExpression = new ConstantFilterExpression("hello");

			Assert.That(constantExpression.Type, Is.EqualTo(typeof (string)));
		}

		[Test]
		public void Constructor_NullValueArgumentNoTypeArgument_TypeIsNull()
		{
			var constantExpression = new ConstantFilterExpression(null);

			Assert.That(constantExpression.Type, Is.Null);
		}

		[Test]
		public void ToString_NullValue_FormattedValueCorrectly()
		{
			var literalExpression = CreateConstantExpression<string>(null);

			Assert.That(literalExpression.ToString(), Is.EqualTo("Null"));
		}

		[Test]
		public void ToString_IntegerValue_FormattedValueCorrectly()
		{
			var literalExpression = CreateConstantExpression(1);

			Assert.That(literalExpression.ToString(), Is.EqualTo("1"));
		}

		[Test]
		public void ToString_DecimalValue_FormattedValueCorrectly()
		{
			var literalExpression = CreateConstantExpression(1.7m);

			Assert.That(literalExpression.ToString(), Is.EqualTo("1.7m"));
		}

		[Test]
		public void ToString_StringValue_FormattedValueCorrectly()
		{
			var literalExpression = CreateConstantExpression("hello");

			Assert.That(literalExpression.ToString(), Is.EqualTo("'hello'"));
		}

		[Test]
		public void ToString_GuidValue_FormattedValueCorrectly()
		{
			var value = Guid.NewGuid();

			var literalExpression = CreateConstantExpression(value);

			Assert.That(literalExpression.ToString(), Is.EqualTo(String.Format("guid'{0}'", value)));
		}

		[Test]
		public void ToString_DateTimeValue_FormattedValueCorrectly()
		{
			var value = DateTime.Now;

			var literalExpression = CreateConstantExpression(value);

			Assert.That(literalExpression.ToString(), Is.EqualTo(String.Format("datetime'{0:yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'ffffff}'", value.ToUniversalTime())));
		}

		[Test]
		public void ToString_DateTimeOffsetValue_FormattedValueCorrectly()
		{
			var value = DateTimeOffset.Now;

			var literalExpression = CreateConstantExpression(value);

			Assert.That(literalExpression.ToString(), Is.EqualTo(String.Format("datetimeoffset'{0:yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fffK}'", value)));
		}

		[Test]
		public void ToString_TimeSpanValue_FormattedValueCorrectly()
		{
			var value = TimeSpan.FromSeconds(3789);

			var literalExpression = CreateConstantExpression(value);

			Assert.That(literalExpression.ToString(), Is.EqualTo(String.Format("time'{0:'P0Y0M'd'DT'h'H'm'M's'.'ffffff'S'}'", value)));
		}
	}
}
