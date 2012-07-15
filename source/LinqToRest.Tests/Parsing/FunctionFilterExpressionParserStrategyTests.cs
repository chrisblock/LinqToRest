// ReSharper disable InconsistentNaming

using System;
using System.Collections.Generic;

using LinqToRest.OData.Filters;
using LinqToRest.OData.Lexing;
using LinqToRest.OData.Parsing;
using LinqToRest.OData.Parsing.Impl;

using NUnit.Framework;

using Rhino.Mocks;

namespace LinqToRest.Tests.Parsing
{
	[TestFixture]
	public class FunctionFilterExpressionParserStrategyTests
	{
		private IFilterExpressionParserStrategy _baseStrategy;

		private IFilterExpressionParserStrategy _functionStrategy;

		[SetUp]
		public void TestSetUp()
		{
			_baseStrategy = MockRepository.GenerateStub<IFilterExpressionParserStrategy>();

			_functionStrategy = new FunctionFilterExpressionParserStrategy(_baseStrategy);
		}

		[Test]
		public void BuildExpresion_NullStack_ThrowsException()
		{
			Assert.That(() => _functionStrategy.BuildExpression(null), Throws.InstanceOf<ArgumentNullException>());
		}

		[Test]
		public void BuildExpresion_EmptyStack_ThrowsException()
		{
			var stack = new Stack<Token>();

			Assert.That(() => _functionStrategy.BuildExpression(stack), Throws.ArgumentException);
		}

		[Test]
		public void BuildExpression_StackContainingNullToken_ThrowsException()
		{
			var stack = new Stack<Token>();

			stack.Push(null);

			Assert.That(() => _functionStrategy.BuildExpression(stack), Throws.ArgumentException);
		}

		[Test]
		public void BuildExpression_StackContainingTokenWithUnparsableValue_ThrowsException()
		{
			var stack = new Stack<Token>();

			stack.Push(TokenType.Function, "Hello, World.");

			Assert.That(() => _functionStrategy.BuildExpression(stack), Throws.ArgumentException);
		}

		[Test]
		public void BuildExpresion_StackWithOnlyFunctionToken_ThrowsException()
		{
			var stack = new Stack<Token>();

			stack.Push(new Token
			{
				TokenType = TokenType.Function,
				Value = "trim"
			});

			Assert.That(() => _functionStrategy.BuildExpression(stack), Throws.ArgumentException);
		}

		[Test]
		public void BuildExpresion_StackWithUnknownFunctionToken_ThrowsException()
		{
			var stack = new Stack<Token>();

			stack.Push(new Token
			{
				TokenType = TokenType.Function,
				Value = Guid.NewGuid().ToString()
			});

			Assert.That(() => _functionStrategy.BuildExpression(stack), Throws.ArgumentException);
		}

		[Test]
		public void BuildExpresion_ValidStackExpression_ReturnsValidArityExpression()
		{
			var stack = new Stack<Token>();

			stack.Push(new Token
			{
				TokenType = TokenType.Name,
				Value = "TestString"
			});

			stack.Push(new Token
			{
				TokenType = TokenType.Function,
				Value = "trim"
			});

			var expression = _functionStrategy.BuildExpression(stack);

			Assert.That(expression, Is.Not.Null);
			Assert.That(expression, Is.TypeOf<MethodCallFilterExpression>());
		}
	}
}
