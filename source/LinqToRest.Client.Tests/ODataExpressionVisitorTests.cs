// ReSharper disable InconsistentNaming

using System;
using System.Linq.Expressions;
using System.Reflection;

using DataModel.Tests;

using LinqToRest.Client.OData.Building;
using LinqToRest.OData.Formatting.Impl;
using LinqToRest.OData.Parsing.Impl;

using NUnit.Framework;

namespace LinqToRest.Client.Tests
{
	[TestFixture]
	public class ODataExpressionVisitorTests
	{
		private ODataFilterExpressionVisitor _visitor;

		private string BuildTranslatedExpression<TReturn>(Expression<Func<TestModel, TReturn>> expression)
		{
			return _visitor.Translate(expression.Body).ToString();
		}

		[SetUp]
		public void TestSetUp()
		{
			_visitor = new ODataFilterExpressionVisitor(new FilterExpressionParserStrategy(), new TypeFormatter());
		}

		[Test]
		public void Translate_AndExpression_TranslatesCorrectly()
		{
			var oDataExpression = BuildTranslatedExpression(s => s.TestString == "hello" && s.TestString.Length < 4);

			Assert.That(oDataExpression, Is.EqualTo("((TestString eq 'hello') and (length(TestString) lt 4))"));
		}

		[Test]
		public void Translate_OrExpression_TranslatesCorrectly()
		{
			var oDataExpression = BuildTranslatedExpression(s => s.TestString == "hello" || s.TestString.Length < 4);

			Assert.That(oDataExpression, Is.EqualTo("((TestString eq 'hello') or (length(TestString) lt 4))"));
		}

		[Test]
		public void Translate_EqualsExpression_TranslatesCorrectly()
		{
			var oDataExpression = BuildTranslatedExpression(s => s.TestString == "hello");

			Assert.That(oDataExpression, Is.EqualTo("(TestString eq 'hello')"));
		}

		[Test]
		public void Translate_NotEqualExpression_TranslatesCorrectly()
		{
			var oDataExpression = BuildTranslatedExpression(s => s.TestString != "hello");

			Assert.That(oDataExpression, Is.EqualTo("(TestString ne 'hello')"));
		}

		[Test]
		public void Translate_LessThanExpression_TranslatesCorrectly()
		{
			var oDataExpression = BuildTranslatedExpression(s => s.TestInt < 4);

			Assert.That(oDataExpression, Is.EqualTo("(TestInt lt 4)"));
		}

		[Test]
		public void Translate_LessThanOrEqualToExpression_TranslatesCorrectly()
		{
			var oDataExpression = BuildTranslatedExpression(s => s.TestInt <= 4);

			Assert.That(oDataExpression, Is.EqualTo("(TestInt le 4)"));
		}

		[Test]
		public void Translate_GreaterThanExpression_TranslatesCorrectly()
		{
			var oDataExpression = BuildTranslatedExpression(s => s.TestInt > 4);

			Assert.That(oDataExpression, Is.EqualTo("(TestInt gt 4)"));
		}

		[Test]
		public void Translate_GreaterThanOrEqualToExpression_TranslatesCorrectly()
		{
			var oDataExpression = BuildTranslatedExpression(s => s.TestInt >= 4);

			Assert.That(oDataExpression, Is.EqualTo("(TestInt ge 4)"));
		}

		[Test]
		public void Translate_AdditionExpression_TranslatesCorrectly()
		{
			var oDataExpression = BuildTranslatedExpression(s => s.TestInt + 4);

			Assert.That(oDataExpression, Is.EqualTo("(TestInt add 4)"));
		}

		[Test]
		public void Translate_IncrementExpression_TranslatesCorrectly()
		{
			var expr = Expression.Increment(Expression.Variable(typeof (int), "s"));

			var oDataExpression = _visitor.Translate(expr).ToString();

			Assert.That(oDataExpression, Is.EqualTo("(s add 1)"));
		}

		[Test]
		public void Translate_SubtractionExpression_TranslatesCorrectly()
		{
			var oDataExpression = BuildTranslatedExpression(s => s.TestInt - 4);

			Assert.That(oDataExpression, Is.EqualTo("(TestInt sub 4)"));
		}

		[Test]
		public void Translate_DecrementExpression_TranslatesCorrectly()
		{
			var expr = Expression.Decrement(Expression.Variable(typeof(int), "s"));

			var oDataExpression = _visitor.Translate(expr).ToString();

			Assert.That(oDataExpression, Is.EqualTo("(s sub 1)"));
		}

		[Test]
		public void Translate_MultiplicationExpression_TranslatesCorrectly()
		{
			var oDataExpression = BuildTranslatedExpression(s => s.TestInt * 4);

			Assert.That(oDataExpression, Is.EqualTo("(TestInt mul 4)"));
		}

		[Test]
		public void Translate_DivisionExpression_TranslatesCorrectly()
		{
			var oDataExpression = BuildTranslatedExpression(s => s.TestInt / 4);

			Assert.That(oDataExpression, Is.EqualTo("(TestInt div 4)"));
		}

		[Test]
		public void Translate_ModulusExpression_TranslatesCorrectly()
		{
			var oDataExpression = BuildTranslatedExpression(s => s.TestInt % 4);

			Assert.That(oDataExpression, Is.EqualTo("(TestInt mod 4)"));
		}

		[Test]
		public void Translate_NegationExpression_TranslatesCorrectly()
		{
			var oDataExpression = BuildTranslatedExpression(s => -s.TestInt > 3);

			Assert.That(oDataExpression, Is.EqualTo("((-(TestInt)) gt 3)"));
		}

		[Test]
		public void Translate_NotExpression_TranslatesCorrectly()
		{
			var oDataExpression = BuildTranslatedExpression(s => !s.TestBoolean);

			Assert.That(oDataExpression, Is.EqualTo("(not(TestBoolean))"));
		}

		[Test]
		public void Translate_GuidExpression_TranslatesCorrectly()
		{
			var guid = Guid.NewGuid();

			var memberAccess = Expression.MakeMemberAccess(Expression.Parameter(typeof(TestModel), "x"), typeof(TestModel).GetProperty("TestGuid", BindingFlags.Instance | BindingFlags.Public));

			var equalsExpression = Expression.Equal(memberAccess, Expression.Constant(guid, typeof(Guid)));

			var oDataExpression = _visitor.Translate(equalsExpression).ToString();

			Assert.That(oDataExpression, Is.EqualTo(String.Format("(TestGuid eq guid'{0}')", guid)));
		}

		[Test]
		public void Translate_DateTimeExpression_TranslatesCorrectly()
		{
			int year = 2000;
			int month = 1;
			int day = 2;
			int hour = 3;
			int minute = 4;
			int second = 5;

			var datetime = new DateTime(year, month, day, hour, minute, second, DateTimeKind.Utc);

			var memberAccess = Expression.MakeMemberAccess(Expression.Parameter(typeof (TestModel), "x"), typeof (TestModel).GetProperty("TestDateTime", BindingFlags.Instance | BindingFlags.Public));

			var equalsExpression = Expression.Equal(memberAccess, Expression.Constant(datetime, typeof (DateTime)));

			var oDataExpression = _visitor.Translate(equalsExpression).ToString();

			Assert.That(oDataExpression, Is.EqualTo(String.Format("(TestDateTime eq datetime'{0:0000}-{1:00}-{2:00}T{3:00}:{4:00}:{5:00}.000000')", year, month, day, hour, minute, second)));
		}

		[Test]
		public void Translate_DecimalExpression_TranslatesCorrectly()
		{
			// TODO: figure out how do deal with it when the value (3.5m) is a variable
			//       it appears that the compiler binds the variable statically to the test fixture
			//       and passes that in as a member expression...which is crazy
			var oDataExpression = BuildTranslatedExpression(s => s.TestDecimal == 3.5m);

			Assert.That(oDataExpression, Is.EqualTo(String.Format("(TestDecimal eq 3.5m)")));
		}

		[Test]
		public void Translate_NullExpression_TranslatesCorrectly()
		{
			var oDataExpression = BuildTranslatedExpression(s => s.TestString == null);

			Assert.That(oDataExpression, Is.EqualTo("(TestString eq Null)"));
		}

		[Test]
		public void Translate_TypeBinaryExpression_TranslatesCorrectly()
		{
			var oDataExpression = BuildTranslatedExpression(s => s.TestInt is int);

			Assert.That(oDataExpression, Is.EqualTo("isof(TestInt, edm.int32)"));
		}

		[Test]
		public void Translate_CastExpression_TranslatesCorrectly()
		{
			var oDataExpression = BuildTranslatedExpression(s => (Type)s.TestObject);

			Assert.That(oDataExpression, Is.EqualTo("cast(TestObject, Type)"));
		}

		[Test]
		public void Translate_AsCastExpression_TranslatesCorrectly()
		{
			var oDataExpression = BuildTranslatedExpression(s => s.TestObject as Type);

			Assert.That(oDataExpression, Is.EqualTo("cast(TestObject, Type)"));
		}

		[Test]
		public void Translate_ConcatenationOperationExpression_TranslatesCorrectly()
		{
			var oDataExpression = BuildTranslatedExpression(s => s.TestString + "hello" + "pi");

			Assert.That(oDataExpression, Is.EqualTo("concat(concat(TestString, 'hello'), 'pi')"));
		}

		[Test]
		public void Translate_ConcatenationMethodCallOperationExpression_TranslatesCorrectly()
		{
			var oDataExpression = BuildTranslatedExpression(s => String.Concat(String.Concat(s.TestString, "hello"), "pi"));

			Assert.That(oDataExpression, Is.EqualTo("concat(concat(TestString, 'hello'), 'pi')"));
		}

		[Test]
		public void Translate_ToUpperMethodCallExpression_TranslatesCorrectly()
		{
			var oDataExpression = BuildTranslatedExpression(s => s.TestString.ToUpper());

			Assert.That(oDataExpression, Is.EqualTo("toupper(TestString)"));
		}

		[Test]
		public void Translate_ToUpperInvariantMethodCallExpression_TranslatesCorrectly()
		{
			var oDataExpression = BuildTranslatedExpression(s => s.TestString.ToUpperInvariant());

			Assert.That(oDataExpression, Is.EqualTo("toupper(TestString)"));
		}

		[Test]
		public void Translate_ToLowerMethodCallExpression_TranslatesCorrectly()
		{
			var oDataExpression = BuildTranslatedExpression(s => s.TestString.ToLower());

			Assert.That(oDataExpression, Is.EqualTo("tolower(TestString)"));
		}

		[Test]
		public void Translate_ToLowerInvariantMethodCallExpression_TranslatesCorrectly()
		{
			var oDataExpression = BuildTranslatedExpression(s => s.TestString.ToLowerInvariant());

			Assert.That(oDataExpression, Is.EqualTo("tolower(TestString)"));
		}

		[Test]
		public void Translate_TrimMethodCallExpression_TranslatesCorrectly()
		{
			var oDataExpression = BuildTranslatedExpression(s => s.TestString.Trim());

			Assert.That(oDataExpression, Is.EqualTo("trim(TestString)"));
		}

		[Test]
		public void Translate_LengthPropertyAccessExpression_TranslatesCorrectly()
		{
			var oDataExpression = BuildTranslatedExpression(s => s.TestString.Length);

			Assert.That(oDataExpression, Is.EqualTo("length(TestString)"));
		}

		[Test]
		public void Translate_YearPropertyAccessExpression_TranslatesCorrectly()
		{
			var oDataExpression = BuildTranslatedExpression(s => s.TestDateTime.Year);

			Assert.That(oDataExpression, Is.EqualTo("year(TestDateTime)"));
		}

		[Test]
		public void Translate_MonthPropertyAccessExpression_TranslatesCorrectly()
		{
			var oDataExpression = BuildTranslatedExpression(s => s.TestDateTime.Month);

			Assert.That(oDataExpression, Is.EqualTo("month(TestDateTime)"));
		}

		[Test]
		public void Translate_DayPropertyAccessExpression_TranslatesCorrectly()
		{
			var oDataExpression = BuildTranslatedExpression(s => s.TestDateTime.Day);

			Assert.That(oDataExpression, Is.EqualTo("day(TestDateTime)"));
		}

		[Test]
		public void Translate_HourPropertyAccessExpression_TranslatesCorrectly()
		{
			var oDataExpression = BuildTranslatedExpression(s => s.TestDateTime.Hour);

			Assert.That(oDataExpression, Is.EqualTo("hour(TestDateTime)"));
		}

		[Test]
		public void Translate_MinutePropertyAccessExpression_TranslatesCorrectly()
		{
			var oDataExpression = BuildTranslatedExpression(s => s.TestDateTime.Minute);

			Assert.That(oDataExpression, Is.EqualTo("minute(TestDateTime)"));
		}

		[Test]
		public void Translate_SecondPropertyAccessExpression_TranslatesCorrectly()
		{
			var oDataExpression = BuildTranslatedExpression(s => s.TestDateTime.Second);

			Assert.That(oDataExpression, Is.EqualTo("second(TestDateTime)"));
		}

		[Test]
		public void Translate_CeilingMethodCallExpression_TranslatesCorrectly()
		{
			var oDataExpression = BuildTranslatedExpression(s => Math.Ceiling(s.TestDecimal));

			Assert.That(oDataExpression, Is.EqualTo("ceiling(TestDecimal)"));
		}

		[Test]
		public void Translate_FloorMethodCallExpression_TranslatesCorrectly()
		{
			var oDataExpression = BuildTranslatedExpression(s => Math.Floor(s.TestDecimal));

			Assert.That(oDataExpression, Is.EqualTo("floor(TestDecimal)"));
		}

		[Test]
		public void Translate_RoundCallExpression_TranslatesCorrectly()
		{
			var oDataExpression = BuildTranslatedExpression(s => Math.Round(s.TestDecimal));

			Assert.That(oDataExpression, Is.EqualTo("round(TestDecimal)"));
		}

		[Test]
		public void Translate_IndexOfMethodCallExpression_TranslatesCorrectly()
		{
			var oDataExpression = BuildTranslatedExpression(s => s.TestString.IndexOf("hello"));

			Assert.That(oDataExpression, Is.EqualTo("indexof(TestString, 'hello')"));
		}

		[Test]
		public void Translate_StartsWithMethodCallExpression_TranslatesCorrectly()
		{
			var oDataExpression = BuildTranslatedExpression(s => s.TestString.StartsWith("hello"));

			Assert.That(oDataExpression, Is.EqualTo("startswith(TestString, 'hello')"));
		}

		[Test]
		public void Translate_EndsWithMethodCallExpression_TranslatesCorrectly()
		{
			var oDataExpression = BuildTranslatedExpression(s => s.TestString.EndsWith("hello"));

			Assert.That(oDataExpression, Is.EqualTo("endswith(TestString, 'hello')"));
		}

		[Test]
		public void Translate_SubstringMethodCallExpression_TranslatesCorrectly()
		{
			var oDataExpression = BuildTranslatedExpression(s => s.TestString.Substring(2));

			Assert.That(oDataExpression, Is.EqualTo("substring(TestString, 2)"));
		}

		[Test]
		public void Translate_ContainsMethodCallExpression_TranslatesCorrectly()
		{
			var oDataExpression = BuildTranslatedExpression(s => s.TestString.Contains("hello"));

			Assert.That(oDataExpression, Is.EqualTo("substringof(TestString, 'hello')"));
		}

		[Test]
		public void Translate_ReplaceMethodCallExpression_TranslatesCorrectly()
		{
			var oDataExpression = BuildTranslatedExpression(s => s.TestString.Replace("hello", "world"));

			Assert.That(oDataExpression, Is.EqualTo("replace(TestString, 'hello', 'world')"));
		}

		[Test]
		public void Translate_MultipleMemberAccess_TranslatesCorrectly()
		{
			var oDataExpression = BuildTranslatedExpression(s => s.TestChild.TestString);

			Assert.That(oDataExpression, Is.EqualTo("TestChild/TestString"));
		}
	}
}
