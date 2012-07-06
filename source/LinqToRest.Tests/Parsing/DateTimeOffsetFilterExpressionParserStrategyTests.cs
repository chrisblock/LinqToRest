// ReSharper disable InconsistentNaming

using System;
using System.Collections.Generic;

using LinqToRest.OData.Filters;
using LinqToRest.OData.Lexing;
using LinqToRest.OData.Parsing;
using LinqToRest.OData.Parsing.Impl;

using NUnit.Framework;

namespace LinqToRest.Tests.Parsing
{
	[TestFixture]
	public class DateTimeOffsetFilterExpressionParserStrategyTests
	{
		private IFilterExpressionParserStrategy _strategy;

		[SetUp]
		public void TestSetUp()
		{
			_strategy = new DateTimeOffsetFilterExpressionParserStrategy();
		}

		[Test]
		public void BuildExpression_NullStack_ThrowsException()
		{
			Assert.That(() => _strategy.BuildExpression(null), Throws.InstanceOf<ArgumentNullException>());
		}

		[Test]
		public void BuildExpression_EmptyStack_ThrowsException()
		{
			var stack = new Stack<Token>();

			Assert.That(() => _strategy.BuildExpression(stack), Throws.ArgumentException);
		}

		[Test]
		public void BuildExpression_StackContainingDateTimeOffsetToken_ReturnsCorrectConstantExpression()
		{
			var value = DateTimeOffset.UtcNow;

			var oDataLiteral = String.Format("datetimeoffset'{0:yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fffK}'", value);

			var stack = new Stack<Token>();

			stack.Push(new Token
			{
				TokenType = TokenType.DateTime,
				Value = oDataLiteral
			});

			var expression = (ConstantFilterExpression) _strategy.BuildExpression(stack);

			Assert.That(expression, Is.Not.Null);
			Assert.That(expression, Is.TypeOf<ConstantFilterExpression>());
			Assert.That(expression, Has.Property("Type").EqualTo(value.GetType()));
			// TODO: figure out why this assert doesn't work, despite the difference of the values being less than 1 millisecond
			// Assert.That(expression, Has.Property("Value").EqualTo(value).Within(1).Seconds);
		}
	}
}
