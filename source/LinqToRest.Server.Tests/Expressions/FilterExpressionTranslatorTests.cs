// ReSharper disable InconsistentNaming

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

using DataModel.Tests;

using LinqToRest.OData.Filters;
using LinqToRest.Server.OData.Expressions;

using NUnit.Framework;

namespace LinqToRest.Server.Tests.Expressions
{
	[TestFixture]
	public class FilterExpressionTranslatorTests
	{
		private FilterExpressionTranslator _filterExpressionTranslator;

		private static Expression GetLambdaBody<T>(Expression<Func<TestModel, T>> expression)
		{
			return expression.Body;
		}

		[SetUp]
		public void TestSetUp()
		{
			var parameter = Expression.Parameter(typeof (TestModel), "x");

			_filterExpressionTranslator = new FilterExpressionTranslator(parameter);
		}

		[Test]
		public void Translate_ConstantStringFilterExpression_ReturnsCorrectLinqExpression()
		{
			var filterExpression = FilterExpression.Constant("hello");

			var expression = _filterExpressionTranslator.Translate(filterExpression);

			var expected = GetLambdaBody<string>(x => "hello");

			Assert.That(expression.ToString(), Is.EqualTo(expected.ToString()));
		}

		[Test]
		public void Translate_InvalidMemberAccess_ThrowsException()
		{
			var filterExpression = FilterExpression.MemberAccess(Guid.NewGuid().ToString());

			Assert.That(() => _filterExpressionTranslator.Translate(filterExpression), Throws.ArgumentException);
		}

		[Test]
		public void Translate_MemberAccess_ThrowsException()
		{
			var filterExpression = FilterExpression.MemberAccess("TestGuid");

			var expression = _filterExpressionTranslator.Translate(filterExpression);

			var expected = GetLambdaBody(x => x.TestGuid);

			Assert.That(expression.ToString(), Is.EqualTo(expected.ToString()));
		}

		[Test]
		public void Translate_MemberOfMemberAccess_ThrowsException()
		{
			var filterExpression = FilterExpression.MemberAccess(FilterExpression.MemberAccess("TestChild"), "TestGuid");

			var expression = _filterExpressionTranslator.Translate(filterExpression);

			var expected = GetLambdaBody(x => x.TestChild.TestGuid);

			Assert.That(expression.ToString(), Is.EqualTo(expected.ToString()));
		}

		[Test]
		public void Translate_EqualsFilterExpressionRightConstant_ReturnsCorrectLinqExpression()
		{
			var filterExpression = FilterExpression.Binary(FilterExpression.MemberAccess("TestString"), FilterExpressionOperator.Equal, FilterExpression.Constant("hello"));

			var expression = _filterExpressionTranslator.Translate(filterExpression);

			var expected = GetLambdaBody(x => x.TestString == "hello");

			Assert.That(expression.ToString(), Is.EqualTo(expected.ToString()));
		}

		[Test]
		public void Translate_EqualsFilterExpressionLeftConstant_ReturnsCorrectLinqExpression()
		{
			var filterExpression = FilterExpression.Binary(FilterExpression.Constant("hello"), FilterExpressionOperator.Equal, FilterExpression.MemberAccess("TestString"));

			var expression = _filterExpressionTranslator.Translate(filterExpression);

			var expected = GetLambdaBody(x => "hello" == x.TestString);

			Assert.That(expression.ToString(), Is.EqualTo(expected.ToString()));
		}

		[Test]
		public void Translate_EqualsFilterExpressionForcesTypeCoercion_ReturnsCorrectLinqExpression()
		{
			var filterExpression = FilterExpression.Binary(FilterExpression.MemberAccess("TestInt"), FilterExpressionOperator.Equal, FilterExpression.Constant(42L));

			var expression = _filterExpressionTranslator.Translate(filterExpression);

			var expected = GetLambdaBody(x => x.TestInt == 42L);

			Assert.That(expression.ToString(), Is.EqualTo(expected.ToString()));
		}

		[Test]
		public void Translate_EqualsFilterExpressionLeftTypeAssignableFromRightType_ReturnsCorrectLinqExpression()
		{
			var filterExpression = FilterExpression.Binary(FilterExpression.MemberAccess("TestObject"), FilterExpressionOperator.Equal, FilterExpression.MemberAccess("TestString"));

			var expression = _filterExpressionTranslator.Translate(filterExpression);

			var expected = GetLambdaBody(x => (string)x.TestObject == x.TestString);

			Assert.That(expression.ToString(), Is.EqualTo(expected.ToString()));
		}

		[Test]
		public void Translate_EqualsFilterExpressionRightTypeAssignableFromLeftType_ReturnsCorrectLinqExpression()
		{
			var filterExpression = FilterExpression.Binary(FilterExpression.MemberAccess("TestString"), FilterExpressionOperator.Equal, FilterExpression.MemberAccess("TestObject"));

			var expression = _filterExpressionTranslator.Translate(filterExpression);

			var expected = GetLambdaBody(x => x.TestString == (string)x.TestObject);

			Assert.That(expression.ToString(), Is.EqualTo(expected.ToString()));
		}

		[Test]
		public void Translate_EqualsFilterExpressionLeftIntRightDecimal_ReturnsCorrectLinqExpression()
		{
			var filterExpression = FilterExpression.Binary(FilterExpression.MemberAccess("TestInt"), FilterExpressionOperator.Equal, FilterExpression.MemberAccess("TestDecimal"));

			var expression = _filterExpressionTranslator.Translate(filterExpression);

			var expected = GetLambdaBody(x => (decimal)x.TestInt == x.TestDecimal);

			Assert.That(expression.ToString(), Is.EqualTo(expected.ToString()));
		}

		[Test]
		public void Translate_EqualsFilterExpressionLeftDecimalRightInt_ReturnsCorrectLinqExpression()
		{
			var filterExpression = FilterExpression.Binary(FilterExpression.MemberAccess("TestDecimal"), FilterExpressionOperator.Equal, FilterExpression.MemberAccess("TestInt"));

			var expression = _filterExpressionTranslator.Translate(filterExpression);

			var expected = GetLambdaBody(x => x.TestDecimal == (decimal)x.TestInt);

			Assert.That(expression.ToString(), Is.EqualTo(expected.ToString()));
		}

		[Test]
		public void Translate_NotEqualToFilterExpression_ReturnsCorrectLinqExpression()
		{
			var filterExpression = FilterExpression.Binary(FilterExpression.MemberAccess("TestString"), FilterExpressionOperator.NotEqual, FilterExpression.Constant("hello"));

			var expression = _filterExpressionTranslator.Translate(filterExpression);

			var expected = GetLambdaBody(x => x.TestString != "hello");

			Assert.That(expression.ToString(), Is.EqualTo(expected.ToString()));
		}

		[Test]
		public void Translate_LessThanFilterExpression_ReturnsCorrectLinqExpression()
		{
			var filterExpression = FilterExpression.Binary(FilterExpression.MemberAccess("TestInt"), FilterExpressionOperator.LessThan, FilterExpression.Constant(42));

			var expression = _filterExpressionTranslator.Translate(filterExpression);

			var expected = GetLambdaBody(x => x.TestInt < 42);

			Assert.That(expression.ToString(), Is.EqualTo(expected.ToString()));
		}

		[Test]
		public void Translate_LessThanOrEqualToFilterExpression_ReturnsCorrectLinqExpression()
		{
			var filterExpression = FilterExpression.Binary(FilterExpression.MemberAccess("TestInt"), FilterExpressionOperator.LessThanOrEqual, FilterExpression.Constant(42));

			var expression = _filterExpressionTranslator.Translate(filterExpression);

			var expected = GetLambdaBody(x => x.TestInt <= 42);

			Assert.That(expression.ToString(), Is.EqualTo(expected.ToString()));
		}

		[Test]
		public void Translate_GreaterThanFilterExpression_ReturnsCorrectLinqExpression()
		{
			var filterExpression = FilterExpression.Binary(FilterExpression.MemberAccess("TestInt"), FilterExpressionOperator.GreaterThan, FilterExpression.Constant(42));

			var expression = _filterExpressionTranslator.Translate(filterExpression);

			var expected = GetLambdaBody(x => x.TestInt > 42);

			Assert.That(expression.ToString(), Is.EqualTo(expected.ToString()));
		}

		[Test]
		public void Translate_GreaterThanOrEqualToFilterExpression_ReturnsCorrectLinqExpression()
		{
			var filterExpression = FilterExpression.Binary(FilterExpression.MemberAccess("TestInt"), FilterExpressionOperator.GreaterThanOrEqual, FilterExpression.Constant(42));

			var expression = _filterExpressionTranslator.Translate(filterExpression);

			var expected = GetLambdaBody(x => x.TestInt >= 42);

			Assert.That(expression.ToString(), Is.EqualTo(expected.ToString()));
		}

		[Test]
		public void Translate_TrimFilterExpression_ReturnsCorrectLinqExpression()
		{
			var filterExpression = FilterExpression.MethodCall(Function.Trim, FilterExpression.MemberAccess("TestString"));

			var expression = _filterExpressionTranslator.Translate(filterExpression);

			var expected = GetLambdaBody(x => x.TestString.Trim());

			Assert.That(expression.ToString(), Is.EqualTo(expected.ToString()));
		}

		[Test]
		public void Translate_IndexOfFilterExpression_ReturnsCorrectLinqExpression()
		{
			var filterExpression = FilterExpression.MethodCall(Function.IndexOf, FilterExpression.MemberAccess("TestString"), FilterExpression.Constant("hi"));

			var expression = _filterExpressionTranslator.Translate(filterExpression);

			var expected = GetLambdaBody(x => x.TestString.IndexOf("hi"));

			Assert.That(expression.ToString(), Is.EqualTo(expected.ToString()));
		}

		[Test]
		public void Translate_UnaryFilterExpression_ReturnsCorrectLinqExpression()
		{
			var filterExpression = FilterExpression.Unary(FilterExpressionOperator.Not, FilterExpression.Binary(FilterExpression.MemberAccess("TestString"), FilterExpressionOperator.Equal, FilterExpression.Constant("hi")));

			var expression = _filterExpressionTranslator.Translate(filterExpression);

			var expected = GetLambdaBody(x => !(x.TestString == "hi"));

			Assert.That(expression.ToString(), Is.EqualTo(expected.ToString()));
		}

		[Test]
		public void Translate_CastMethodCallFilterExpression_ReturnsCorrectLinqExpression()
		{
			var filterExpression = FilterExpression.MethodCall(Function.Cast, FilterExpression.MemberAccess("TestObject"), FilterExpression.Constant(typeof (string)));

			var expression = _filterExpressionTranslator.Translate(filterExpression);

			var expected = GetLambdaBody(x => (string) x.TestObject);

			Assert.That(expression.ToString(), Is.EqualTo(expected.ToString()));
		}
	}
}
