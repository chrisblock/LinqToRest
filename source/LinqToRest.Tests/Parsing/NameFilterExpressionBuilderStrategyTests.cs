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
	public class NameFilterExpressionBuilderStrategyTests
	{
		private IFilterExpressionParserStrategy _strategy;

		[SetUp]
		public void TestSetUp()
		{
			_strategy = new NameFilterExpressionParserStrategy();
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
		public void BuildExpression_StackContainingNameToken_ReturnsCorrectConstantExpression()
		{
			var value = "HelloWorld";

			var oDataLiteral = String.Format("{0}", value);

			var stack = new Stack<Token>();

			stack.Push(TokenType.Name, oDataLiteral);

			var expression = _strategy.BuildExpression(stack);

			Assert.That(expression, Is.Not.Null);
			Assert.That(expression, Is.TypeOf<MemberAccessFilterExpression>());
			Assert.That(expression, Has.Property("Instance").Null);
			Assert.That(expression, Has.Property("Member").EqualTo(value));
		}
	}
}
