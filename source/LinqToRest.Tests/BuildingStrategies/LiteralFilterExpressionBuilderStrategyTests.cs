// ReSharper disable InconsistentNaming

using System;
using System.Collections.Generic;

using LinqToRest.OData.Building.Strategies;
using LinqToRest.OData.Building.Strategies.Impl;
using LinqToRest.OData.Filters;
using LinqToRest.OData.Literals;

using NUnit.Framework;

namespace LinqToRest.Tests.BuildingStrategies
{
	[TestFixture]
	public class LiteralFilterExpressionBuilderStrategyTests
	{
		private IFilterExpressionBuilderStrategy _literalStrategy;

		[SetUp]
		public void TestSetUp()
		{
			_literalStrategy = new LiteralFilterExpressionBuilderStrategy();
		}

		[Test]
		public void BuildExpression_NullStack_ThrowsException()
		{
			Assert.That(() => _literalStrategy.BuildExpression(null), Throws.InstanceOf<ArgumentNullException>());
		}

		[Test]
		public void BuildExpression_EmptyStack_ThrowsException()
		{
			var stack = new Stack<Token>();

			Assert.That(() => _literalStrategy.BuildExpression(stack), Throws.ArgumentException);
		}

		[Test]
		public void BuildExpression_StackContainingODataStringLiteral_ReturnsCorrectConstantExpression()
		{
			var value = "hello world";

			var oDataLiteral = String.Format("'{0}'", value);

			var stack = new Stack<Token>();

			stack.Push(new Token
			{
				TokenType = TokenType.String,
				Value = oDataLiteral
			});

			var expression = _literalStrategy.BuildExpression(stack);

			Assert.That(expression, Is.Not.Null);
			Assert.That(expression, Is.TypeOf<ConstantFilterExpression>());
			Assert.That(expression, Has.Property("Type").EqualTo(value.GetType()));
			Assert.That(expression, Has.Property("Value").EqualTo(value));
		}

		[Test]
		public void BuildExpression_StackContainingODataIntegerLiteral_ReturnsCorrectConstantExpression()
		{
			var value = 42;

			var oDataLiteral = String.Format("{0}", value);

			var stack = new Stack<Token>();

			stack.Push(new Token
			{
				TokenType = TokenType.Integer,
				Value = oDataLiteral
			});

			var expression = _literalStrategy.BuildExpression(stack);

			Assert.That(expression, Is.Not.Null);
			Assert.That(expression, Is.TypeOf<ConstantFilterExpression>());
			Assert.That(expression, Has.Property("Type").EqualTo(value.GetType()));
			Assert.That(expression, Has.Property("Value").EqualTo(value));
		}

		[Test]
		public void BuildExpression_StackContainingODataDecimalLiteral_ReturnsCorrectConstantExpression()
		{
			var value = 3.14m;

			var oDataLiteral = String.Format("{0}m", value);

			var stack = new Stack<Token>();

			stack.Push(new Token
			{
				TokenType = TokenType.Decimal,
				Value = oDataLiteral
			});

			var expression = _literalStrategy.BuildExpression(stack);

			Assert.That(expression, Is.Not.Null);
			Assert.That(expression, Is.TypeOf<ConstantFilterExpression>());
			Assert.That(expression, Has.Property("Type").EqualTo(value.GetType()));
			Assert.That(expression, Has.Property("Value").EqualTo(value));
		}

		[Test]
		public void BuildExpression_StackContainingODataDoubleLiteral_ReturnsCorrectConstantExpression()
		{
			var value = 3.14;

			var oDataLiteral = String.Format("{0}", value);

			var stack = new Stack<Token>();

			stack.Push(new Token
			{
				TokenType = TokenType.Double,
				Value = oDataLiteral
			});

			var expression = _literalStrategy.BuildExpression(stack);

			Assert.That(expression, Is.Not.Null);
			Assert.That(expression, Is.TypeOf<ConstantFilterExpression>());
			Assert.That(expression, Has.Property("Type").EqualTo(value.GetType()));
			Assert.That(expression, Has.Property("Value").EqualTo(value));
		}

		[Test]
		public void BuildExpression_StackContainingODataDateTimeLiteral_ReturnsCorrectConstantExpression()
		{
			var value = DateTime.UtcNow;

			var oDataLiteral = String.Format("datetime'{0:yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'ffffff}'", value);

			var stack = new Stack<Token>();

			stack.Push(new Token
			{
				TokenType = TokenType.DateTime,
				Value = oDataLiteral
			});

			var expression = _literalStrategy.BuildExpression(stack);

			Assert.That(expression, Is.Not.Null);
			Assert.That(expression, Is.TypeOf<ConstantFilterExpression>());
			Assert.That(expression, Has.Property("Type").EqualTo(value.GetType()));
			Assert.That(expression, Has.Property("Value").EqualTo(value.ToLocalTime()).Within(1).Seconds);
		}
	}
}
