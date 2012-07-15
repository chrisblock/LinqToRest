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
	public class TimeFilterExpressionParserStrategyTests
	{
		private IFilterExpressionParserStrategy _strategy;

		[SetUp]
		public void TestSetUp()
		{
			_strategy = new TimeFilterExpressionParserStrategy();
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
		public void BuildExpression_StackContainingNullToken_ThrowsException()
		{
			var stack = new Stack<Token>();

			stack.Push(null);

			Assert.That(() => _strategy.BuildExpression(stack), Throws.ArgumentException);
		}

		[Test]
		public void BuildExpression_StackContainingTokenWithUnparsableValue_ThrowsException()
		{
			var stack = new Stack<Token>();

			stack.Push(TokenType.Time, "Hello, World.");

			Assert.That(() => _strategy.BuildExpression(stack), Throws.ArgumentException);
		}

		[Test]
		public void BuildExpression_StackContainingTimeToken_ReturnsCorrectConstantExpression()
		{
			var stack = new Stack<Token>();

			var value = DateTime.Now.TimeOfDay;

			stack.Push(TokenType.Time, String.Format("time'{0:'P0Y0M'd'DT'h'H'm'M's'.'ffffff'S'}'", value));

			var expression = _strategy.BuildExpression(stack);

			Assert.That(expression, Is.Not.Null);
			Assert.That(expression, Is.TypeOf<ConstantFilterExpression>());
			Assert.That(expression, Has.Property("Type").EqualTo(value.GetType()));
			Assert.That(expression, Has.Property("Value").EqualTo(value).Within(1).Milliseconds);
		}
	}
}
