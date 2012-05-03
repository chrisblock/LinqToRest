using System;
using System.Collections.Generic;
using System.Linq;

using LinqToRest.OData.Parsing;
using LinqToRest.OData.Parsing.Impl;

using NUnit.Framework;

namespace LinqToRest.Tests
{
	[TestFixture]
	public class ODataQueryExpressionBuilderTests
	{
		private static readonly Guid TestGuid = Guid.NewGuid();

		private static IQueryable<TestModel> BuildSource()
		{
			return new List<TestModel>
			{
				new TestModel
				{
					TestBoolean = true,
					TestDateTime = new DateTime(2012, 1, 1, 1, 1, 10),
					TestDateTimeOffset = new DateTimeOffset(2008, 2, 2, 2, 2, 4, 0, TimeSpan.FromSeconds(0)),
					TestTime = TimeSpan.FromSeconds(0),
					TestDecimal = 3.9m,
					TestGuid = Guid.NewGuid(),
					TestInt = 3,
					TestObject = new object(),
					TestString = "hello'",
					TestChild = null
				},
				new TestModel
				{
					TestBoolean = true,
					TestDateTime = new DateTime(2012, 2, 1, 1, 1, 1),
					TestDateTimeOffset = new DateTimeOffset(2009, 3, 2, 2, 2, 2, 0, TimeSpan.FromSeconds(0)),
					TestTime = TimeSpan.FromSeconds(30),
					TestDecimal = 3.3m,
					TestGuid = Guid.NewGuid(),
					TestInt = 4,
					TestObject = String.Empty,
					TestString = "hello  ",
					TestChild = null
				},
				new TestModel
				{
					TestBoolean = true,
					TestDateTime = new DateTime(2012, 3, 4, 1, 1, 1),
					TestDateTimeOffset = new DateTimeOffset(2010, 4, 9, 2, 2, 2, 0, TimeSpan.FromSeconds(0)),
					TestTime = TimeSpan.FromMinutes(30),
					TestDecimal = 2.5m,
					TestGuid = Guid.NewGuid(),
					TestInt = 5,
					TestObject = "Hello World",
					TestString = "goodbye",
					TestChild = null
				},
				new TestModel
				{
					TestBoolean = false,
					TestDateTime = new DateTime(2012, 1, 5, 6, 1, 1),
					TestDateTimeOffset = new DateTimeOffset(2010, 2, 1, 4, 2, 2, 0, TimeSpan.FromSeconds(0)),
					TestTime = TimeSpan.FromHours(12),
					TestDecimal = 1.9m,
					TestGuid = TestGuid,
					TestInt = 6,
					TestObject = null,
					TestString = "omg",
					TestChild = null
				},
				new TestModel
				{
					TestBoolean = true,
					TestDateTime = new DateTime(2010, 1, 1, 7, 8, 1),
					TestDateTimeOffset = new DateTimeOffset(2010, 2, 2, 6, 7, 2, 0, TimeSpan.FromSeconds(0)),
					TestTime = TimeSpan.FromDays(300),
					TestDecimal = 4.0m,
					TestGuid = Guid.NewGuid(),
					TestInt = 7,
					TestObject = new object(),
					TestString = "wtf",
					TestChild = null
				},
				new TestModel
				{
					TestBoolean = false,
					TestDateTime = new DateTime(2011, 1, 1, 1, 9, 10),
					TestDateTimeOffset = new DateTimeOffset(2010, 2, 2, 2, 3, 5, 0, TimeSpan.FromSeconds(0)),
					TestTime = TimeSpan.FromDays(2 * 365),
					TestDecimal = 9.9m,
					TestGuid = Guid.NewGuid(),
					TestInt = 8,
					TestObject = new object(),
					TestString = "OMG",
					TestChild = null
				}
			}.AsQueryable();
		}

		private readonly IQueryable<TestModel> _source = BuildSource();

		// TODO: write so many tests that it hurts
		[Test]
		public void BuildExpression_TestBooleanEqualsFalse_ReturnsTwoResults()
		{
			var type = typeof(TestModel);

			IODataQueryExpressionBuilder expressionBuilder = new ODataQueryExpressionBuilder(type);

			var expr = expressionBuilder.BuildExpression("TestBoolean eq False");

			var lambda = (Func<TestModel, bool>)expr.Compile();

			var result = _source.Where(lambda).ToList();

			Assert.That(result.Count, Is.EqualTo(2));
		}

		[Test]
		public void BuildExpression_TestBooleanDoesNotEqualFalse_ReturnsFourResults()
		{
			var type = typeof(TestModel);

			IODataQueryExpressionBuilder expressionBuilder = new ODataQueryExpressionBuilder(type);

			var expr = expressionBuilder.BuildExpression("TestBoolean ne False");

			var lambda = (Func<TestModel, bool>)expr.Compile();

			var result = _source.Where(lambda).ToList();

			Assert.That(result.Count, Is.EqualTo(4));
		}

		[Test]
		public void BuildExpression_TestBooleanEqualsTrue_ReturnsFourResults()
		{
			var type = typeof(TestModel);

			IODataQueryExpressionBuilder expressionBuilder = new ODataQueryExpressionBuilder(type);

			var expr = expressionBuilder.BuildExpression("TestBoolean eq true");

			var lambda = (Func<TestModel, bool>)expr.Compile();

			var result = _source.Where(lambda).ToList();

			Assert.That(result.Count, Is.EqualTo(4));
		}

		[Test]
		public void BuildExpression_TestBooleanDoesNotEqualTrue_ReturnsTwoResults()
		{
			var type = typeof(TestModel);

			IODataQueryExpressionBuilder expressionBuilder = new ODataQueryExpressionBuilder(type);

			var expr = expressionBuilder.BuildExpression("TestBoolean ne true");

			var lambda = (Func<TestModel, bool>)expr.Compile();

			var result = _source.Where(lambda).ToList();

			Assert.That(result.Count, Is.EqualTo(2));
		}

		[Test]
		public void BuildExpression_TestDateTimeYearDoesNotEqualTwentyTwelve_ReturnsTwoResults()
		{
			var type = typeof(TestModel);

			IODataQueryExpressionBuilder expressionBuilder = new ODataQueryExpressionBuilder(type);

			var expr = expressionBuilder.BuildExpression("year(TestDateTime) ne 2012");

			var lambda = (Func<TestModel, bool>)expr.Compile();

			var result = _source.Where(lambda).ToList();

			Assert.That(result.Count, Is.EqualTo(2));
		}

		[Test]
		public void BuildExpression_TestDateTimeMonthDoesNotEqualOne_ReturnsTwoResults()
		{
			var type = typeof(TestModel);

			IODataQueryExpressionBuilder expressionBuilder = new ODataQueryExpressionBuilder(type);

			var expr = expressionBuilder.BuildExpression("month(TestDateTime) ne 1");

			var lambda = (Func<TestModel, bool>)expr.Compile();

			var result = _source.Where(lambda).ToList();

			Assert.That(result.Count, Is.EqualTo(2));
		}

		[Test]
		public void BuildExpression_TestDateTimeDayDoesNotEqualOne_ReturnsTwoResults()
		{
			var type = typeof(TestModel);

			IODataQueryExpressionBuilder expressionBuilder = new ODataQueryExpressionBuilder(type);

			var expr = expressionBuilder.BuildExpression("day(TestDateTime) ne 1");

			var lambda = (Func<TestModel, bool>)expr.Compile();

			var result = _source.Where(lambda).ToList();

			Assert.That(result.Count, Is.EqualTo(2));
		}

		[Test]
		public void BuildExpression_TestDateTimeHourDoesNotEqualOne_ReturnsTwoResults()
		{
			var type = typeof(TestModel);

			IODataQueryExpressionBuilder expressionBuilder = new ODataQueryExpressionBuilder(type);

			var expr = expressionBuilder.BuildExpression("hour(TestDateTime) ne 1");

			var lambda = (Func<TestModel, bool>)expr.Compile();

			var result = _source.Where(lambda).ToList();

			Assert.That(result.Count, Is.EqualTo(2));
		}

		[Test]
		public void BuildExpression_TestDateTimeMinuteDoesNotEqualOne_ReturnsTwoResults()
		{
			var type = typeof(TestModel);

			IODataQueryExpressionBuilder expressionBuilder = new ODataQueryExpressionBuilder(type);

			var expr = expressionBuilder.BuildExpression("minute(TestDateTime) ne 1");

			var lambda = (Func<TestModel, bool>)expr.Compile();

			var result = _source.Where(lambda).ToList();

			Assert.That(result.Count, Is.EqualTo(2));
		}

		[Test]
		public void BuildExpression_TestDateTimeSecondDoesNotEqualOne_ReturnsTwoResults()
		{
			var type = typeof(TestModel);

			IODataQueryExpressionBuilder expressionBuilder = new ODataQueryExpressionBuilder(type);

			var expr = expressionBuilder.BuildExpression("second(TestDateTime) ne 1");

			var lambda = (Func<TestModel, bool>)expr.Compile();

			var result = _source.Where(lambda).ToList();

			Assert.That(result.Count, Is.EqualTo(2));
		}

		[Test]
		public void BuildExpression_TestDateTimeOffsetYearDoesNotEqualTwentyTen_ReturnsTwoResults()
		{
			var type = typeof(TestModel);

			IODataQueryExpressionBuilder expressionBuilder = new ODataQueryExpressionBuilder(type);

			var expr = expressionBuilder.BuildExpression("year(TestDateTimeOffset) ne 2010");

			var lambda = (Func<TestModel, bool>)expr.Compile();

			var result = _source.Where(lambda).ToList();

			Assert.That(result.Count, Is.EqualTo(2));
		}

		[Test]
		public void BuildExpression_TestDateTimeOffsetMonthDoesNotEqualTwo_ReturnsTwoResults()
		{
			var type = typeof(TestModel);

			IODataQueryExpressionBuilder expressionBuilder = new ODataQueryExpressionBuilder(type);

			var expr = expressionBuilder.BuildExpression("month(TestDateTimeOffset) ne 2");

			var lambda = (Func<TestModel, bool>)expr.Compile();

			var result = _source.Where(lambda).ToList();

			Assert.That(result.Count, Is.EqualTo(2));
		}

		[Test]
		public void BuildExpression_TestDateTimeOffsetDayDoesNotEqualTwo_ReturnsTwoResults()
		{
			var type = typeof(TestModel);

			IODataQueryExpressionBuilder expressionBuilder = new ODataQueryExpressionBuilder(type);

			var expr = expressionBuilder.BuildExpression("day(TestDateTimeOffset) ne 2");

			var lambda = (Func<TestModel, bool>)expr.Compile();

			var result = _source.Where(lambda).ToList();

			Assert.That(result.Count, Is.EqualTo(2));
		}

		[Test]
		public void BuildExpression_TestDateTimeOffsetHourDoesNotEqualTwo_ReturnsTwoResults()
		{
			var type = typeof(TestModel);

			IODataQueryExpressionBuilder expressionBuilder = new ODataQueryExpressionBuilder(type);

			var expr = expressionBuilder.BuildExpression("hour(TestDateTimeOffset) ne 2");

			var lambda = (Func<TestModel, bool>)expr.Compile();

			var result = _source.Where(lambda).ToList();

			Assert.That(result.Count, Is.EqualTo(2));
		}

		[Test]
		public void BuildExpression_TestDateTimeOffsetMinuteDoesNotEqualTwo_ReturnsTwoResults()
		{
			var type = typeof(TestModel);

			IODataQueryExpressionBuilder expressionBuilder = new ODataQueryExpressionBuilder(type);

			var expr = expressionBuilder.BuildExpression("minute(TestDateTimeOffset) ne 2");

			var lambda = (Func<TestModel, bool>)expr.Compile();

			var result = _source.Where(lambda).ToList();

			Assert.That(result.Count, Is.EqualTo(2));
		}

		[Test]
		public void BuildExpression_TestDateTimeOffsetSecondDoesNotEqualTwo_ReturnsTwoResults()
		{
			var type = typeof(TestModel);

			IODataQueryExpressionBuilder expressionBuilder = new ODataQueryExpressionBuilder(type);

			var expr = expressionBuilder.BuildExpression("second(TestDateTimeOffset) ne 2");

			var lambda = (Func<TestModel, bool>)expr.Compile();

			var result = _source.Where(lambda).ToList();

			Assert.That(result.Count, Is.EqualTo(2));
		}

		[Ignore]
		[Test]
		public void BuildExpression_TestTimeYearsLessThanOne_ReturnsFiveResults()
		{
			var type = typeof(TestModel);

			IODataQueryExpressionBuilder expressionBuilder = new ODataQueryExpressionBuilder(type);

			var expr = expressionBuilder.BuildExpression("years(TestTime) lt 1");

			var lambda = (Func<TestModel, bool>)expr.Compile();

			var result = _source.Where(lambda).ToList();

			Assert.That(result.Count, Is.EqualTo(5));
		}

		[Test]
		public void BuildExpression_TestTimeDaysLessThanOne_ReturnsFourResults()
		{
			var type = typeof(TestModel);

			IODataQueryExpressionBuilder expressionBuilder = new ODataQueryExpressionBuilder(type);

			var expr = expressionBuilder.BuildExpression("days(TestTime) lt 1");

			var lambda = (Func<TestModel, bool>)expr.Compile();

			var result = _source.Where(lambda).ToList();

			Assert.That(result.Count, Is.EqualTo(4));
		}

		[Test]
		public void BuildExpression_TestTimeHoursLessThanOne_ReturnsThreeResults()
		{
			var type = typeof(TestModel);

			IODataQueryExpressionBuilder expressionBuilder = new ODataQueryExpressionBuilder(type);

			var expr = expressionBuilder.BuildExpression("hours(TestTime) lt 1");

			var lambda = (Func<TestModel, bool>)expr.Compile();

			var result = _source.Where(lambda).ToList();

			Assert.That(result.Count, Is.EqualTo(3));
		}

		[Test]
		public void BuildExpression_TestTimeMinutesLessThanOne_ReturnsTwoResults()
		{
			var type = typeof(TestModel);

			IODataQueryExpressionBuilder expressionBuilder = new ODataQueryExpressionBuilder(type);

			var expr = expressionBuilder.BuildExpression("minutes(TestTime) lt 1");

			var lambda = (Func<TestModel, bool>)expr.Compile();

			var result = _source.Where(lambda).ToList();

			Assert.That(result.Count, Is.EqualTo(2));
		}

		[Test]
		public void BuildExpression_TestTimeSecondsLessThanOne_ReturnsOneResult()
		{
			var type = typeof(TestModel);

			IODataQueryExpressionBuilder expressionBuilder = new ODataQueryExpressionBuilder(type);

			var expr = expressionBuilder.BuildExpression("seconds(TestTime) lt 1");

			var lambda = (Func<TestModel, bool>)expr.Compile();

			var result = _source.Where(lambda).ToList();

			Assert.That(result.Count, Is.EqualTo(1));
		}

		[Test]
		public void BuildExpression_TestDecimalRoundEqualsThree_ReturnsTwoResults()
		{
			var type = typeof(TestModel);

			IODataQueryExpressionBuilder expressionBuilder = new ODataQueryExpressionBuilder(type);

			var expr = expressionBuilder.BuildExpression("round(TestDecimal) eq 3.0m");

			var lambda = (Func<TestModel, bool>)expr.Compile();

			var result = _source.Where(lambda).ToList();

			Assert.That(result.Count, Is.EqualTo(1));
		}

		[Test]
		public void BuildExpression_TestDecimalFloorEqualsFour_ReturnsOneResult()
		{
			var type = typeof(TestModel);

			IODataQueryExpressionBuilder expressionBuilder = new ODataQueryExpressionBuilder(type);

			var expr = expressionBuilder.BuildExpression("floor(TestDecimal) eq 4.0m");

			var lambda = (Func<TestModel, bool>)expr.Compile();

			var result = _source.Where(lambda).ToList();

			Assert.That(result.Count, Is.EqualTo(1));
		}

		[Test]
		public void BuildExpression_TestDecimalCeilingEqualsTwo_ReturnsOneResult()
		{
			var type = typeof(TestModel);

			IODataQueryExpressionBuilder expressionBuilder = new ODataQueryExpressionBuilder(type);

			var expr = expressionBuilder.BuildExpression("ceiling(TestDecimal) eq 2.0m");

			var lambda = (Func<TestModel, bool>)expr.Compile();

			var result = _source.Where(lambda).ToList();

			Assert.That(result.Count, Is.EqualTo(1));
		}

		[Test]
		public void BuildExpression_TestStringEqualsHello_ReturnsSingleResult()
		{
			var type = typeof (TestModel);

			IODataQueryExpressionBuilder expressionBuilder = new ODataQueryExpressionBuilder(type);

			var expr = expressionBuilder.BuildExpression("TestString eq 'hello\\''");

			var lambda = (Func<TestModel, bool>) expr.Compile();

			var result = _source.Where(lambda).ToList();

			Assert.That(result.Count, Is.EqualTo(1));
		}

		[Test]
		public void BuildExpression_TestStringLengthEqualsThree_ReturnsThreResults()
		{
			var type = typeof(TestModel);

			IODataQueryExpressionBuilder expressionBuilder = new ODataQueryExpressionBuilder(type);

			var expr = expressionBuilder.BuildExpression("length(TestString) eq 3");

			var lambda = (Func<TestModel, bool>)expr.Compile();

			var result = _source.Where(lambda).ToList();

			Assert.That(result.Count, Is.EqualTo(3));
		}

		[Test]
		public void BuildExpression_TestStringToLowerEqualsLowerCaseOmg_ReturnsTwoResults()
		{
			var type = typeof(TestModel);

			IODataQueryExpressionBuilder expressionBuilder = new ODataQueryExpressionBuilder(type);

			var expr = expressionBuilder.BuildExpression("tolower(TestString) eq 'omg'");

			var lambda = (Func<TestModel, bool>)expr.Compile();

			var result = _source.Where(lambda).ToList();

			Assert.That(result.Count, Is.EqualTo(2));
		}

		[Test]
		public void BuildExpression_TestStringToUpperEqualsUpperCaseOmg_ReturnsTwoResults()
		{
			var type = typeof(TestModel);

			IODataQueryExpressionBuilder expressionBuilder = new ODataQueryExpressionBuilder(type);

			var expr = expressionBuilder.BuildExpression("toupper(TestString) eq 'OMG'");

			var lambda = (Func<TestModel, bool>)expr.Compile();

			var result = _source.Where(lambda).ToList();

			Assert.That(result.Count, Is.EqualTo(2));
		}

		[Test]
		public void BuildExpression_TestStringTrimEqualsHello_ReturnsOneResult()
		{
			var type = typeof(TestModel);

			IODataQueryExpressionBuilder expressionBuilder = new ODataQueryExpressionBuilder(type);

			var expr = expressionBuilder.BuildExpression("trim(TestString) eq 'hello'");

			var lambda = (Func<TestModel, bool>)expr.Compile();

			var result = _source.Where(lambda).ToList();

			Assert.That(result.Count, Is.EqualTo(1));
		}

		[Test]
		public void BuildExpression_TestStringCocatenatedWithOmgEqualsOmgOmg_ReturnsOneResult()
		{
			var type = typeof(TestModel);

			IODataQueryExpressionBuilder expressionBuilder = new ODataQueryExpressionBuilder(type);

			var expr = expressionBuilder.BuildExpression("concat(TestString, 'omg') eq 'omgomg'");

			var lambda = (Func<TestModel, bool>)expr.Compile();

			var result = _source.Where(lambda).ToList();

			Assert.That(result.Count, Is.EqualTo(1));
		}

		[Test]
		public void BuildExpression_TestIntegerLessThanFive_ReturnsTwoResults()
		{
			var type = typeof(TestModel);

			IODataQueryExpressionBuilder expressionBuilder = new ODataQueryExpressionBuilder(type);

			var expr = expressionBuilder.BuildExpression("TestInt lt 5");

			var lambda = (Func<TestModel, bool>)expr.Compile();

			var result = _source.Where(lambda).ToList();

			Assert.That(result.Count, Is.EqualTo(2));
		}

		[Test]
		public void BuildExpression_TestIntegerLessThanOrEqualToFive_ReturnsThreeResults()
		{
			var type = typeof(TestModel);

			IODataQueryExpressionBuilder expressionBuilder = new ODataQueryExpressionBuilder(type);

			var expr = expressionBuilder.BuildExpression("TestInt le 5");

			var lambda = (Func<TestModel, bool>)expr.Compile();

			var result = _source.Where(lambda).ToList();

			Assert.That(result.Count, Is.EqualTo(3));
		}

		[Test]
		public void BuildExpression_TestIntegerGreaterThanFive_ReturnsThreeResults()
		{
			var type = typeof(TestModel);

			IODataQueryExpressionBuilder expressionBuilder = new ODataQueryExpressionBuilder(type);

			var expr = expressionBuilder.BuildExpression("TestInt gt 5");

			var lambda = (Func<TestModel, bool>)expr.Compile();

			var result = _source.Where(lambda).ToList();

			Assert.That(result.Count, Is.EqualTo(3));
		}

		[Test]
		public void BuildExpression_TestIntegerGreaterThanOrEqualToFive_ReturnsFourResults()
		{
			var type = typeof(TestModel);

			IODataQueryExpressionBuilder expressionBuilder = new ODataQueryExpressionBuilder(type);

			var expr = expressionBuilder.BuildExpression("TestInt ge 5");

			var lambda = (Func<TestModel, bool>)expr.Compile();

			var result = _source.Where(lambda).ToList();

			Assert.That(result.Count, Is.EqualTo(4));
		}

		[Test]
		public void BuildExpression_TestGuidDoesNotEqualTestGuidConstant_ReturnsFiveResults()
		{
			var type = typeof(TestModel);

			IODataQueryExpressionBuilder expressionBuilder = new ODataQueryExpressionBuilder(type);

			var expr = expressionBuilder.BuildExpression(String.Format("TestGuid ne guid'{0}'", TestGuid));

			var lambda = (Func<TestModel, bool>)expr.Compile();

			var result = _source.Where(lambda).ToList();

			Assert.That(result.Count, Is.EqualTo(5));
		}

		[Test]
		public void BuildExpression_TestObjectIsOfString_ReturnsTwoResults()
		{
			var type = typeof(TestModel);

			IODataQueryExpressionBuilder expressionBuilder = new ODataQueryExpressionBuilder(type);

			var expr = expressionBuilder.BuildExpression("isof(TestObject, System.String)");

			var lambda = (Func<TestModel, bool>)expr.Compile();

			var result = _source.Where(lambda).ToList();

			Assert.That(result.Count, Is.EqualTo(2));
		}
	}
}
