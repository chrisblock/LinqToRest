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
	public class StringFilterExpressionParserStrategyTests
	{
		private IFilterExpressionParserStrategy _strategy;

		[SetUp]
		public void TestSetUp()
		{
			_strategy = new StringFilterExpressionParserStrategy();
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
		public void BuildExpression_StackContainingTokenWithUnparsableEmptyValue_ThrowsException()
		{
			var stack = new Stack<Token>();

			stack.Push(TokenType.String, String.Empty);

			Assert.That(() => _strategy.BuildExpression(stack), Throws.ArgumentException);
		}

		[Test]
		public void BuildExpression_StackContainingTokenWithUnparsableWhitespaceValue_ThrowsException()
		{
			var stack = new Stack<Token>();

			stack.Push(TokenType.String, " \t\r\n");

			Assert.That(() => _strategy.BuildExpression(stack), Throws.ArgumentException);
		}

		[Test]
		public void BuildExpression_StackContainingTokenWithUnparsableNonQuotedValue_ThrowsException()
		{
			var stack = new Stack<Token>();

			stack.Push(TokenType.String, "Hello, World.'");

			Assert.That(() => _strategy.BuildExpression(stack), Throws.ArgumentException);
		}

		[Test]
		public void BuildExpression_StackContainingTokenWithUnparsableUnterminatedQuoteValue_ThrowsException()
		{
			var stack = new Stack<Token>();

			stack.Push(TokenType.String, "'Hello, World.");

			Assert.That(() => _strategy.BuildExpression(stack), Throws.ArgumentException);
		}

		[Test]
		public void BuildExpression_StackContainingStringToken_ReturnsCorrectConstantExpression()
		{
			var value = "hello world";

			var oDataLiteral = String.Format("'{0}'", value);

			var stack = new Stack<Token>();

			stack.Push(TokenType.String, oDataLiteral);

			var expression = _strategy.BuildExpression(stack);

			Assert.That(expression, Is.Not.Null);
			Assert.That(expression, Is.TypeOf<ConstantFilterExpression>());
			Assert.That(expression, Has.Property("Type").EqualTo(value.GetType()));
			Assert.That(expression, Has.Property("Value").EqualTo(value));
		}
	}
}
