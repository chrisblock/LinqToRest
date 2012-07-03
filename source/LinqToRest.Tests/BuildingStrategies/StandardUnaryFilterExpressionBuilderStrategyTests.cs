// ReSharper disable InconsistentNaming

using System;
using System.Collections.Generic;

using LinqToRest.OData.Building.Strategies;
using LinqToRest.OData.Building.Strategies.Impl;
using LinqToRest.OData.Filters;
using LinqToRest.OData.Literals;

using NUnit.Framework;

using Rhino.Mocks;

namespace LinqToRest.Tests.BuildingStrategies
{
	[TestFixture]
	public class StandardUnaryFilterExpressionBuilderStrategyTests
	{
		private IFilterExpressionBuilderStrategy _baseStrategy;

		private IFilterExpressionBuilderStrategy _unaryStrategy;

		[SetUp]
		public void TestSetUp()
		{
			_baseStrategy = MockRepository.GenerateStub<IFilterExpressionBuilderStrategy>();

			_unaryStrategy = new StandardUnaryFilterExpressionBuilderStrategy(_baseStrategy);
		}

		[Test]
		public void BuildExpression_NullStack_ThrowsException()
		{
			Assert.That(() => _unaryStrategy.BuildExpression(null), Throws.InstanceOf<ArgumentNullException>());
		}

		[Test]
		public void BuildExpression_EmptyStack_ThrowsException()
		{
			var stack = new Stack<Token>();

			Assert.That(() => _unaryStrategy.BuildExpression(stack), Throws.ArgumentException);
		}

		[Test]
		public void BuildExpression_StackWithOnlyOperator_ThrowsException()
		{
			var stack = new Stack<Token>();

			stack.Push(new Token
			{
				TokenType = TokenType.UnaryOperator,
				Value = "not"
			});

			Assert.That(() => _unaryStrategy.BuildExpression(stack), Throws.Exception);
		}

		[Test]
		public void BuildExpression_StackWithOnlyOperand_ThrowsException()
		{
			var stack = new Stack<Token>();

			stack.Push(new Token
			{
				TokenType = TokenType.Boolean,
				Value = "true"
			});

			Assert.That(() => _unaryStrategy.BuildExpression(stack), Throws.Exception);
		}

		[Test]
		public void BuildExpression_NonUnaryOperator_ThrowsException()
		{
			var stack = new Stack<Token>();

			stack.Push(new Token
			{
				TokenType = TokenType.Name,
				Value = "TestDecimal"
			});

			stack.Push(new Token
			{
				TokenType = TokenType.BinaryOperator,
				Value = "ne"
			});

			Assert.That(() => _unaryStrategy.BuildExpression(stack), Throws.Exception);
		}

		[Test]
		public void BuildExpression_ProperStack_ReturnsCorrectUnaryFilterExpression()
		{
			_baseStrategy
				.Stub(x => x.BuildExpression(null))
				.IgnoreArguments()
				.Return(FilterExpression.Constant(true));

			var stack = new Stack<Token>();

			stack.Push(new Token
			{
				TokenType = TokenType.Boolean,
				Value = "true"
			});

			stack.Push(new Token
			{
				TokenType = TokenType.UnaryOperator,
				Value = "not"
			});

			var expression = _unaryStrategy.BuildExpression(stack);

			Assert.That(expression, Is.TypeOf<UnaryFilterExpression>());
			Assert.That(expression.ExpressionType, Is.EqualTo(FilterExpressionType.Unary));
			Assert.That(((UnaryFilterExpression)expression).Operator, Is.EqualTo(FilterExpressionOperator.Not));
			Assert.That(((UnaryFilterExpression)expression).Operand, Is.TypeOf<ConstantFilterExpression>());
		}
	}
}
