// ReSharper disable InconsistentNaming

using System;
using System.Collections.Generic;
using System.Linq;

using LinqToRest.OData;
using LinqToRest.Server.OData.Expressions;
using LinqToRest.Server.OData.Expressions.Impl;
using LinqToRest.Server.OData.Parsing;
using LinqToRest.Server.OData.Parsing.Impl;

using NUnit.Framework;

namespace LinqToRest.Server.Tests
{
	[TestFixture]
	public class ODataQueryFilterExpressionBuilderTests
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

		private readonly IODataQueryPartParserStrategy _parser = new ODataQueryPartParserStrategy();

		private readonly IFilterExpressionBuilder _filterExpressionBuilder = new FilterExpressionBuilder();

		// TODO: write so many tests that it hurts
		[Test]
		public void BuildExpression_TestBooleanEqualsFalse_ReturnsTwoResults()
		{
			var type = typeof(TestModel);

			var filter = _parser.Parse(ODataQueryPartType.Filter, "TestBoolean eq False") as FilterQueryPart;

			var expr = _filterExpressionBuilder.BuildExpression(type, filter.FilterExpression);

			var lambda = (Func<TestModel, bool>)expr.Compile();

			var result = _source.Where(lambda).ToList();

			Assert.That(result.Count, Is.EqualTo(2));
		}

		[Test]
		public void BuildExpression_TestBooleanDoesNotEqualFalse_ReturnsFourResults()
		{
			var type = typeof(TestModel);

			var filter = _parser.Parse(ODataQueryPartType.Filter, "TestBoolean ne False") as FilterQueryPart;

			var expr = _filterExpressionBuilder.BuildExpression(type, filter.FilterExpression);

			var lambda = (Func<TestModel, bool>)expr.Compile();

			var result = _source.Where(lambda).ToList();

			Assert.That(result.Count, Is.EqualTo(4));
		}

		[Test]
		public void BuildExpression_TestBooleanEqualsTrue_ReturnsFourResults()
		{
			var type = typeof(TestModel);

			var filter = _parser.Parse(ODataQueryPartType.Filter, "TestBoolean eq true") as FilterQueryPart;

			var expr = _filterExpressionBuilder.BuildExpression(type, filter.FilterExpression);

			var lambda = (Func<TestModel, bool>)expr.Compile();

			var result = _source.Where(lambda).ToList();

			Assert.That(result.Count, Is.EqualTo(4));
		}

		[Test]
		public void BuildExpression_TestBooleanDoesNotEqualTrue_ReturnsTwoResults()
		{
			var type = typeof(TestModel);

			var filter = _parser.Parse(ODataQueryPartType.Filter, "TestBoolean ne true") as FilterQueryPart;

			var expr = _filterExpressionBuilder.BuildExpression(type, filter.FilterExpression);

			var lambda = (Func<TestModel, bool>)expr.Compile();

			var result = _source.Where(lambda).ToList();

			Assert.That(result.Count, Is.EqualTo(2));
		}

		[Test]
		public void BuildExpression_TestDateTimeYearDoesNotEqualTwentyTwelve_ReturnsTwoResults()
		{
			var type = typeof(TestModel);

			var filter = _parser.Parse(ODataQueryPartType.Filter, "year(TestDateTime) ne 2012") as FilterQueryPart;

			var expr = _filterExpressionBuilder.BuildExpression(type, filter.FilterExpression);

			var lambda = (Func<TestModel, bool>)expr.Compile();

			var result = _source.Where(lambda).ToList();

			Assert.That(result.Count, Is.EqualTo(2));
		}

		[Test]
		public void BuildExpression_TestDateTimeMonthDoesNotEqualOne_ReturnsTwoResults()
		{
			var type = typeof(TestModel);

			var filter = _parser.Parse(ODataQueryPartType.Filter, "month(TestDateTime) ne 1") as FilterQueryPart;

			var expr = _filterExpressionBuilder.BuildExpression(type, filter.FilterExpression);

			var lambda = (Func<TestModel, bool>)expr.Compile();

			var result = _source.Where(lambda).ToList();

			Assert.That(result.Count, Is.EqualTo(2));
		}

		[Test]
		public void BuildExpression_TestDateTimeDayDoesNotEqualOne_ReturnsTwoResults()
		{
			var type = typeof(TestModel);

			var filter = _parser.Parse(ODataQueryPartType.Filter, "day(TestDateTime) ne 1") as FilterQueryPart;

			var expr = _filterExpressionBuilder.BuildExpression(type, filter.FilterExpression);

			var lambda = (Func<TestModel, bool>)expr.Compile();

			var result = _source.Where(lambda).ToList();

			Assert.That(result.Count, Is.EqualTo(2));
		}

		[Test]
		public void BuildExpression_TestDateTimeHourDoesNotEqualOne_ReturnsTwoResults()
		{
			var type = typeof(TestModel);

			var filter = _parser.Parse(ODataQueryPartType.Filter, "hour(TestDateTime) ne 1") as FilterQueryPart;

			var expr = _filterExpressionBuilder.BuildExpression(type, filter.FilterExpression);

			var lambda = (Func<TestModel, bool>)expr.Compile();

			var result = _source.Where(lambda).ToList();

			Assert.That(result.Count, Is.EqualTo(2));
		}

		[Test]
		public void BuildExpression_TestDateTimeMinuteDoesNotEqualOne_ReturnsTwoResults()
		{
			var type = typeof(TestModel);

			var filter = _parser.Parse(ODataQueryPartType.Filter, "minute(TestDateTime) ne 1") as FilterQueryPart;

			var expr = _filterExpressionBuilder.BuildExpression(type, filter.FilterExpression);

			var lambda = (Func<TestModel, bool>)expr.Compile();

			var result = _source.Where(lambda).ToList();

			Assert.That(result.Count, Is.EqualTo(2));
		}

		[Test]
		public void BuildExpression_TestDateTimeSecondDoesNotEqualOne_ReturnsTwoResults()
		{
			var type = typeof(TestModel);

			var filter = _parser.Parse(ODataQueryPartType.Filter, "second(TestDateTime) ne 1") as FilterQueryPart;

			var expr = _filterExpressionBuilder.BuildExpression(type, filter.FilterExpression);

			var lambda = (Func<TestModel, bool>)expr.Compile();

			var result = _source.Where(lambda).ToList();

			Assert.That(result.Count, Is.EqualTo(2));
		}

		[Test]
		public void BuildExpression_TestDateTimeOffsetYearDoesNotEqualTwentyTen_ReturnsTwoResults()
		{
			var type = typeof(TestModel);

			var filter = _parser.Parse(ODataQueryPartType.Filter, "year(TestDateTimeOffset) ne 2010") as FilterQueryPart;

			var expr = _filterExpressionBuilder.BuildExpression(type, filter.FilterExpression);

			var lambda = (Func<TestModel, bool>)expr.Compile();

			var result = _source.Where(lambda).ToList();

			Assert.That(result.Count, Is.EqualTo(2));
		}

		[Test]
		public void BuildExpression_TestDateTimeOffsetMonthDoesNotEqualTwo_ReturnsTwoResults()
		{
			var type = typeof(TestModel);

			var filter = _parser.Parse(ODataQueryPartType.Filter, "month(TestDateTimeOffset) ne 2") as FilterQueryPart;

			var expr = _filterExpressionBuilder.BuildExpression(type, filter.FilterExpression);

			var lambda = (Func<TestModel, bool>)expr.Compile();

			var result = _source.Where(lambda).ToList();

			Assert.That(result.Count, Is.EqualTo(2));
		}

		[Test]
		public void BuildExpression_TestDateTimeOffsetDayDoesNotEqualTwo_ReturnsTwoResults()
		{
			var type = typeof(TestModel);

			var filter = _parser.Parse(ODataQueryPartType.Filter, "day(TestDateTimeOffset) ne 2") as FilterQueryPart;

			var expr = _filterExpressionBuilder.BuildExpression(type, filter.FilterExpression);

			var lambda = (Func<TestModel, bool>)expr.Compile();

			var result = _source.Where(lambda).ToList();

			Assert.That(result.Count, Is.EqualTo(2));
		}

		[Test]
		public void BuildExpression_TestDateTimeOffsetHourDoesNotEqualTwo_ReturnsTwoResults()
		{
			var type = typeof(TestModel);

			var filter = _parser.Parse(ODataQueryPartType.Filter, "hour(TestDateTimeOffset) ne 2") as FilterQueryPart;

			var expr = _filterExpressionBuilder.BuildExpression(type, filter.FilterExpression);

			var lambda = (Func<TestModel, bool>)expr.Compile();

			var result = _source.Where(lambda).ToList();

			Assert.That(result.Count, Is.EqualTo(2));
		}

		[Test]
		public void BuildExpression_TestDateTimeOffsetMinuteDoesNotEqualTwo_ReturnsTwoResults()
		{
			var type = typeof(TestModel);

			var filter = _parser.Parse(ODataQueryPartType.Filter, "minute(TestDateTimeOffset) ne 2") as FilterQueryPart;

			var expr = _filterExpressionBuilder.BuildExpression(type, filter.FilterExpression);

			var lambda = (Func<TestModel, bool>)expr.Compile();

			var result = _source.Where(lambda).ToList();

			Assert.That(result.Count, Is.EqualTo(2));
		}

		[Test]
		public void BuildExpression_TestDateTimeOffsetSecondDoesNotEqualTwo_ReturnsTwoResults()
		{
			var type = typeof(TestModel);

			var filter = _parser.Parse(ODataQueryPartType.Filter, "second(TestDateTimeOffset) ne 2") as FilterQueryPart;

			var expr = _filterExpressionBuilder.BuildExpression(type, filter.FilterExpression);

			var lambda = (Func<TestModel, bool>)expr.Compile();

			var result = _source.Where(lambda).ToList();

			Assert.That(result.Count, Is.EqualTo(2));
		}

		[Ignore]
		[Test]
		public void BuildExpression_TestTimeYearsLessThanOne_ReturnsFiveResults()
		{
			var type = typeof(TestModel);

			var filter = _parser.Parse(ODataQueryPartType.Filter, "years(TestTime) lt 1") as FilterQueryPart;

			var expr = _filterExpressionBuilder.BuildExpression(type, filter.FilterExpression);

			var lambda = (Func<TestModel, bool>)expr.Compile();

			var result = _source.Where(lambda).ToList();

			Assert.That(result.Count, Is.EqualTo(5));
		}

		[Test]
		public void BuildExpression_TestTimeDaysLessThanOne_ReturnsFourResults()
		{
			var type = typeof(TestModel);

			var filter = _parser.Parse(ODataQueryPartType.Filter, "days(TestTime) lt 1") as FilterQueryPart;

			var expr = _filterExpressionBuilder.BuildExpression(type, filter.FilterExpression);

			var lambda = (Func<TestModel, bool>)expr.Compile();

			var result = _source.Where(lambda).ToList();

			Assert.That(result.Count, Is.EqualTo(4));
		}

		[Test]
		public void BuildExpression_TestTimeHoursLessThanOne_ReturnsThreeResults()
		{
			var type = typeof(TestModel);

			var filter = _parser.Parse(ODataQueryPartType.Filter, "hours(TestTime) lt 1") as FilterQueryPart;

			var expr = _filterExpressionBuilder.BuildExpression(type, filter.FilterExpression);

			var lambda = (Func<TestModel, bool>)expr.Compile();

			var result = _source.Where(lambda).ToList();

			Assert.That(result.Count, Is.EqualTo(3));
		}

		[Test]
		public void BuildExpression_TestTimeMinutesLessThanOne_ReturnsTwoResults()
		{
			var type = typeof(TestModel);

			var filter = _parser.Parse(ODataQueryPartType.Filter, "minutes(TestTime) lt 1") as FilterQueryPart;

			var expr = _filterExpressionBuilder.BuildExpression(type, filter.FilterExpression);

			var lambda = (Func<TestModel, bool>)expr.Compile();

			var result = _source.Where(lambda).ToList();

			Assert.That(result.Count, Is.EqualTo(2));
		}

		[Test]
		public void BuildExpression_TestTimeSecondsLessThanOne_ReturnsOneResult()
		{
			var type = typeof(TestModel);

			var filter = _parser.Parse(ODataQueryPartType.Filter, "seconds(TestTime) lt 1") as FilterQueryPart;

			var expr = _filterExpressionBuilder.BuildExpression(type, filter.FilterExpression);

			var lambda = (Func<TestModel, bool>)expr.Compile();

			var result = _source.Where(lambda).ToList();

			Assert.That(result.Count, Is.EqualTo(1));
		}

		[Test]
		public void BuildExpression_TestDecimalRoundEqualsThree_ReturnsTwoResults()
		{
			var type = typeof(TestModel);

			var filter = _parser.Parse(ODataQueryPartType.Filter, "round(TestDecimal) eq 3.0m") as FilterQueryPart;

			var expr = _filterExpressionBuilder.BuildExpression(type, filter.FilterExpression);

			var lambda = (Func<TestModel, bool>)expr.Compile();

			var result = _source.Where(lambda).ToList();

			Assert.That(result.Count, Is.EqualTo(1));
		}

		[Test]
		public void BuildExpression_TestDecimalFloorEqualsFour_ReturnsOneResult()
		{
			var type = typeof(TestModel);

			var filter = _parser.Parse(ODataQueryPartType.Filter, "floor(TestDecimal) eq 4.0m") as FilterQueryPart;

			var expr = _filterExpressionBuilder.BuildExpression(type, filter.FilterExpression);

			var lambda = (Func<TestModel, bool>)expr.Compile();

			var result = _source.Where(lambda).ToList();

			Assert.That(result.Count, Is.EqualTo(1));
		}

		[Test]
		public void BuildExpression_TestDecimalCeilingEqualsTwo_ReturnsOneResult()
		{
			var type = typeof(TestModel);

			var filter = _parser.Parse(ODataQueryPartType.Filter, "ceiling(TestDecimal) eq 2.0m") as FilterQueryPart;

			var expr = _filterExpressionBuilder.BuildExpression(type, filter.FilterExpression);

			var lambda = (Func<TestModel, bool>)expr.Compile();

			var result = _source.Where(lambda).ToList();

			Assert.That(result.Count, Is.EqualTo(1));
		}

		[Test]
		public void BuildExpression_TestStringEqualsHello_ReturnsSingleResult()
		{
			var type = typeof (TestModel);

			var filter = _parser.Parse(ODataQueryPartType.Filter, "TestString eq 'hello\\''") as FilterQueryPart;

			var expr = _filterExpressionBuilder.BuildExpression(type, filter.FilterExpression);

			var lambda = (Func<TestModel, bool>) expr.Compile();

			var result = _source.Where(lambda).ToList();

			Assert.That(result.Count, Is.EqualTo(1));
		}

		[Test]
		public void BuildExpression_TestStringLengthEqualsThree_ReturnsThreResults()
		{
			var type = typeof(TestModel);

			var filter = _parser.Parse(ODataQueryPartType.Filter, "length(TestString) eq 3") as FilterQueryPart;

			var expr = _filterExpressionBuilder.BuildExpression(type, filter.FilterExpression);

			var lambda = (Func<TestModel, bool>)expr.Compile();

			var result = _source.Where(lambda).ToList();

			Assert.That(result.Count, Is.EqualTo(3));
		}

		[Test]
		public void BuildExpression_TestStringToLowerEqualsLowerCaseOmg_ReturnsTwoResults()
		{
			var type = typeof(TestModel);

			var filter = _parser.Parse(ODataQueryPartType.Filter, "tolower(TestString) eq 'omg'") as FilterQueryPart;

			var expr = _filterExpressionBuilder.BuildExpression(type, filter.FilterExpression);

			var lambda = (Func<TestModel, bool>)expr.Compile();

			var result = _source.Where(lambda).ToList();

			Assert.That(result.Count, Is.EqualTo(2));
		}

		[Test]
		public void BuildExpression_TestStringToUpperEqualsUpperCaseOmg_ReturnsTwoResults()
		{
			var type = typeof(TestModel);

			var filter = _parser.Parse(ODataQueryPartType.Filter, "toupper(TestString) eq 'OMG'") as FilterQueryPart;

			var expr = _filterExpressionBuilder.BuildExpression(type, filter.FilterExpression);

			var lambda = (Func<TestModel, bool>)expr.Compile();

			var result = _source.Where(lambda).ToList();

			Assert.That(result.Count, Is.EqualTo(2));
		}

		[Test]
		public void BuildExpression_TestStringTrimEqualsHello_ReturnsOneResult()
		{
			var type = typeof(TestModel);

			var filter = _parser.Parse(ODataQueryPartType.Filter, "trim(TestString) eq 'hello'") as FilterQueryPart;

			var expr = _filterExpressionBuilder.BuildExpression(type, filter.FilterExpression);

			var lambda = (Func<TestModel, bool>)expr.Compile();

			var result = _source.Where(lambda).ToList();

			Assert.That(result.Count, Is.EqualTo(1));
		}

		[Test]
		public void BuildExpression_TestStringCocatenatedWithOmgEqualsOmgOmg_ReturnsOneResult()
		{
			var type = typeof(TestModel);

			var filter = _parser.Parse(ODataQueryPartType.Filter, "concat(TestString, 'omg') eq 'omgomg'") as FilterQueryPart;

			var expr = _filterExpressionBuilder.BuildExpression(type, filter.FilterExpression);

			var lambda = (Func<TestModel, bool>)expr.Compile();

			var result = _source.Where(lambda).ToList();

			Assert.That(result.Count, Is.EqualTo(1));
		}

		[Test]
		public void BuildExpression_TestIntegerLessThanFive_ReturnsTwoResults()
		{
			var type = typeof(TestModel);

			var filter = _parser.Parse(ODataQueryPartType.Filter, "TestInt lt 5") as FilterQueryPart;

			var expr = _filterExpressionBuilder.BuildExpression(type, filter.FilterExpression);

			var lambda = (Func<TestModel, bool>)expr.Compile();

			var result = _source.Where(lambda).ToList();

			Assert.That(result.Count, Is.EqualTo(2));
		}

		[Test]
		public void BuildExpression_TestIntegerLessThanOrEqualToFive_ReturnsThreeResults()
		{
			var type = typeof(TestModel);

			var filter = _parser.Parse(ODataQueryPartType.Filter, "TestInt le 5") as FilterQueryPart;

			var expr = _filterExpressionBuilder.BuildExpression(type, filter.FilterExpression);

			var lambda = (Func<TestModel, bool>)expr.Compile();

			var result = _source.Where(lambda).ToList();

			Assert.That(result.Count, Is.EqualTo(3));
		}

		[Test]
		public void BuildExpression_TestIntegerGreaterThanFive_ReturnsThreeResults()
		{
			var type = typeof(TestModel);

			var filter = _parser.Parse(ODataQueryPartType.Filter, "TestInt gt 5") as FilterQueryPart;

			var expr = _filterExpressionBuilder.BuildExpression(type, filter.FilterExpression);

			var lambda = (Func<TestModel, bool>)expr.Compile();

			var result = _source.Where(lambda).ToList();

			Assert.That(result.Count, Is.EqualTo(3));
		}

		[Test]
		public void BuildExpression_TestIntegerGreaterThanOrEqualToFive_ReturnsFourResults()
		{
			var type = typeof(TestModel);

			var filter = _parser.Parse(ODataQueryPartType.Filter, "TestInt ge 5") as FilterQueryPart;

			var expr = _filterExpressionBuilder.BuildExpression(type, filter.FilterExpression);

			var lambda = (Func<TestModel, bool>)expr.Compile();

			var result = _source.Where(lambda).ToList();

			Assert.That(result.Count, Is.EqualTo(4));
		}

		[Test]
		public void BuildExpression_TestGuidDoesNotEqualTestGuidConstant_ReturnsFiveResults()
		{
			var type = typeof(TestModel);

			var filter = _parser.Parse(ODataQueryPartType.Filter, String.Format("TestGuid ne guid'{0}'", TestGuid)) as FilterQueryPart;

			var expr = _filterExpressionBuilder.BuildExpression(type, filter.FilterExpression);

			var lambda = (Func<TestModel, bool>)expr.Compile();

			var result = _source.Where(lambda).ToList();

			Assert.That(result.Count, Is.EqualTo(5));
		}

		[Test]
		public void BuildExpression_TestObjectIsOfString_ReturnsTwoResults()
		{
			var type = typeof(TestModel);

			var filter = _parser.Parse(ODataQueryPartType.Filter, "isof(TestObject, System.String)") as FilterQueryPart;

			var expr = _filterExpressionBuilder.BuildExpression(type, filter.FilterExpression);

			var lambda = (Func<TestModel, bool>)expr.Compile();

			var result = _source.Where(lambda).ToList();

			Assert.That(result.Count, Is.EqualTo(2));
		}
	}
}
