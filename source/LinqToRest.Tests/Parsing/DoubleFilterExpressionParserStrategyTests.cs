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
	public class DoubleFilterExpressionParserStrategyTests
	{
		private IFilterExpressionParserStrategy _strategy;

		[SetUp]
		public void TestSetUp()
		{
			_strategy = new DoubleFilterExpressionParserStrategy();
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

			stack.Push(TokenType.Double, "Hello, World.");

			Assert.That(() => _strategy.BuildExpression(stack), Throws.ArgumentException);
		}

		[Test]
		public void BuildExpression_StackContainingDoubleToken_ReturnsCorrectConstantExpression()
		{
			var value = 3.14;

			var oDataLiteral = String.Format("{0}", value);

			var stack = new Stack<Token>();

			stack.Push(new Token
			{
				TokenType = TokenType.Double,
				Value = oDataLiteral
			});

			var expression = _strategy.BuildExpression(stack);

			Assert.That(expression, Is.Not.Null);
			Assert.That(expression, Is.TypeOf<ConstantFilterExpression>());
			Assert.That(expression, Has.Property("Type").EqualTo(value.GetType()));
			Assert.That(expression, Has.Property("Value").EqualTo(value));
		}
	}
}
