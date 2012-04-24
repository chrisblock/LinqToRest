using System;
using System.Linq.Expressions;

using LinqToRest.OData;

using NUnit.Framework;

namespace LinqToRest.Tests
{
	[TestFixture]
	public class ODataExpressionVisitorTests
	{
		[Test]
		public void Translate_AndExpression_TranslatesCorrectly()
		{
			var visitor = new ODataExpressionVisitor();

			Expression<Func<string, bool>> expression = s => s == "hello" && s.Length < 4;

			var oDataExpression = visitor.Translate(expression.Body);

			Assert.That(oDataExpression, Is.EqualTo("((s eq 'hello') and (length(s) lt 4))"));
		}

		[Test]
		public void Translate_OrExpression_TranslatesCorrectly()
		{
			var visitor = new ODataExpressionVisitor();

			Expression<Func<string, bool>> expression = s => s == "hello" || s.Length < 4;

			var oDataExpression = visitor.Translate(expression.Body);

			Assert.That(oDataExpression, Is.EqualTo("((s eq 'hello') or (length(s) lt 4))"));
		}

		[Test]
		public void Translate_EqualsExpression_TranslatesCorrectly()
		{
			var visitor = new ODataExpressionVisitor();

			Expression<Func<string, bool>> expression = s => s == "hello";

			var oDataExpression = visitor.Translate(expression.Body);

			Assert.That(oDataExpression, Is.EqualTo("(s eq 'hello')"));
		}

		[Test]
		public void Translate_NotEqualExpression_TranslatesCorrectly()
		{
			var visitor = new ODataExpressionVisitor();

			Expression<Func<string, bool>> expression = s => s != "hello";

			var oDataExpression = visitor.Translate(expression.Body);

			Assert.That(oDataExpression, Is.EqualTo("(s ne 'hello')"));
		}

		[Test]
		public void Translate_LessThanExpression_TranslatesCorrectly()
		{
			var visitor = new ODataExpressionVisitor();

			Expression<Func<int, bool>> expression = s => s < 4;

			var oDataExpression = visitor.Translate(expression.Body);

			Assert.That(oDataExpression, Is.EqualTo("(s lt 4)"));
		}

		[Test]
		public void Translate_LessThanOrEqualToExpression_TranslatesCorrectly()
		{
			var visitor = new ODataExpressionVisitor();

			Expression<Func<int, bool>> expression = s => s <= 4;

			var oDataExpression = visitor.Translate(expression.Body);

			Assert.That(oDataExpression, Is.EqualTo("(s le 4)"));
		}

		[Test]
		public void Translate_GreaterThanExpression_TranslatesCorrectly()
		{
			var visitor = new ODataExpressionVisitor();

			Expression<Func<int, bool>> expression = s => s > 4;

			var oDataExpression = visitor.Translate(expression.Body);

			Assert.That(oDataExpression, Is.EqualTo("(s gt 4)"));
		}

		[Test]
		public void Translate_GreaterThanOrEqualToExpression_TranslatesCorrectly()
		{
			var visitor = new ODataExpressionVisitor();

			Expression<Func<int, bool>> expression = s => s >= 4;

			var oDataExpression = visitor.Translate(expression.Body);

			Assert.That(oDataExpression, Is.EqualTo("(s ge 4)"));
		}

		[Test]
		public void Translate_AdditionExpression_TranslatesCorrectly()
		{
			var visitor = new ODataExpressionVisitor();

			Expression<Func<int, int>> expression = s => s + 4;

			var oDataExpression = visitor.Translate(expression.Body);

			Assert.That(oDataExpression, Is.EqualTo("(s add 4)"));
		}

		[Test]
		public void Translate_IncrementExpression_TranslatesCorrectly()
		{
			var visitor = new ODataExpressionVisitor();

			var expr = Expression.Increment(Expression.Variable(typeof (int), "s"));

			var oDataExpression = visitor.Translate(expr);

			Assert.That(oDataExpression, Is.EqualTo("(s add 1)"));
		}

		[Test]
		public void Translate_SubtractionExpression_TranslatesCorrectly()
		{
			var visitor = new ODataExpressionVisitor();

			Expression<Func<int, int>> expression = s => s - 4;

			var oDataExpression = visitor.Translate(expression.Body);

			Assert.That(oDataExpression, Is.EqualTo("(s sub 4)"));
		}

		[Test]
		public void Translate_DecrementExpression_TranslatesCorrectly()
		{
			var visitor = new ODataExpressionVisitor();

			var expr = Expression.Decrement(Expression.Variable(typeof(int), "s"));

			var oDataExpression = visitor.Translate(expr);

			Assert.That(oDataExpression, Is.EqualTo("(s sub 1)"));
		}

		[Test]
		public void Translate_MultiplicationExpression_TranslatesCorrectly()
		{
			var visitor = new ODataExpressionVisitor();

			Expression<Func<int, int>> expression = s => s * 4;

			var oDataExpression = visitor.Translate(expression.Body);

			Assert.That(oDataExpression, Is.EqualTo("(s mul 4)"));
		}

		[Test]
		public void Translate_DivisionExpression_TranslatesCorrectly()
		{
			var visitor = new ODataExpressionVisitor();

			Expression<Func<int, int>> expression = s => s / 4;

			var oDataExpression = visitor.Translate(expression.Body);

			Assert.That(oDataExpression, Is.EqualTo("(s div 4)"));
		}

		[Test]
		public void Translate_ModulusExpression_TranslatesCorrectly()
		{
			var visitor = new ODataExpressionVisitor();

			Expression<Func<int, int>> expression = s => s % 4;

			var oDataExpression = visitor.Translate(expression.Body);

			Assert.That(oDataExpression, Is.EqualTo("(s mod 4)"));
		}

		[Test]
		public void Translate_NegationExpression_TranslatesCorrectly()
		{
			var visitor = new ODataExpressionVisitor();

			Expression<Func<int, bool>> expression = s => -s > 3;

			var oDataExpression = visitor.Translate(expression.Body);

			Assert.That(oDataExpression, Is.EqualTo("((-(s)) gt 3)"));
		}

		[Test]
		public void Translate_NotExpression_TranslatesCorrectly()
		{
			var visitor = new ODataExpressionVisitor();

			Expression<Func<bool, bool>> expression = s => !s;

			var oDataExpression = visitor.Translate(expression.Body);

			Assert.That(oDataExpression, Is.EqualTo("(not(s))"));
		}

		[Test]
		public void Translate_GuidExpression_TranslatesCorrectly()
		{
			var visitor = new ODataExpressionVisitor();

			var guid = Guid.NewGuid();

			var expression = Expression.Constant(guid, typeof (Guid));

			var oDataExpression = visitor.Translate(expression);

			Assert.That(oDataExpression, Is.EqualTo(String.Format("guid'{0}'", guid)));
		}

		[Test]
		public void Translate_DateTimeExpression_TranslatesCorrectly()
		{
			var visitor = new ODataExpressionVisitor();

			int year = 2000;
			int month = 1;
			int day = 2;
			int hour = 3;
			int minute = 4;
			int second = 5;

			var datetime = new DateTime(year, month, day, hour, minute, second, DateTimeKind.Utc);

			var expression = Expression.Constant(datetime, typeof(DateTime));

			var oDataExpression = visitor.Translate(expression);

			Assert.That(oDataExpression, Is.EqualTo(String.Format("datetime'{0:0000}-{1:00}-{2:00}T{3:00}:{4:00}:{5:00}Z'", year, month, day, hour, minute, second)));
		}

		[Test]
		public void Translate_DecimalExpression_TranslatesCorrectly()
		{
			var visitor = new ODataExpressionVisitor();

			var dec = 3.5m;

			var expression = Expression.Constant(dec, typeof(decimal));

			var oDataExpression = visitor.Translate(expression);

			Assert.That(oDataExpression, Is.EqualTo(String.Format("{0}m", dec)));
		}

		[Test]
		public void Translate_NullExpression_TranslatesCorrectly()
		{
			var visitor = new ODataExpressionVisitor();

			var expression = Expression.Constant(null, typeof(string));

			var oDataExpression = visitor.Translate(expression);

			Assert.That(oDataExpression, Is.EqualTo("Null"));
		}

		[Test]
		public void Translate_TypeBinaryExpression_TranslatesCorrectly()
		{
			var visitor = new ODataExpressionVisitor();

			Expression<Func<long, bool>> expression = s => s is int;

			var oDataExpression = visitor.Translate(expression.Body);

			Assert.That(oDataExpression, Is.EqualTo("isof(s, Int32)"));
		}

		[Test]
		public void Translate_CastExpression_TranslatesCorrectly()
		{
			var visitor = new ODataExpressionVisitor();

			Expression<Func<object, Type>> expression = s => (Type)s;

			var oDataExpression = visitor.Translate(expression.Body);

			Assert.That(oDataExpression, Is.EqualTo("cast(s, Type)"));
		}

		[Test]
		public void Translate_AsCastExpression_TranslatesCorrectly()
		{
			var visitor = new ODataExpressionVisitor();

			Expression<Func<object, Type>> expression = s => s as Type;

			var oDataExpression = visitor.Translate(expression.Body);

			Assert.That(oDataExpression, Is.EqualTo("cast(s, Type)"));
		}

		[Test]
		public void Translate_ToUpperMethodCallExpression_TranslatesCorrectly()
		{
			var visitor = new ODataExpressionVisitor();

			Expression<Func<string, string>> expression = s => s.ToUpper();

			var oDataExpression = visitor.Translate(expression.Body);

			Assert.That(oDataExpression, Is.EqualTo("toupper(s)"));
		}

		[Test]
		public void Translate_ToUpperInvariantMethodCallExpression_TranslatesCorrectly()
		{
			var visitor = new ODataExpressionVisitor();

			Expression<Func<string, string>> expression = s => s.ToUpperInvariant();

			var oDataExpression = visitor.Translate(expression.Body);

			Assert.That(oDataExpression, Is.EqualTo("toupper(s)"));
		}

		[Test]
		public void Translate_ToLowerMethodCallExpression_TranslatesCorrectly()
		{
			var visitor = new ODataExpressionVisitor();

			Expression<Func<string, string>> expression = s => s.ToLower();

			var oDataExpression = visitor.Translate(expression.Body);

			Assert.That(oDataExpression, Is.EqualTo("tolower(s)"));
		}

		[Test]
		public void Translate_ToLowerInvariantMethodCallExpression_TranslatesCorrectly()
		{
			var visitor = new ODataExpressionVisitor();

			Expression<Func<string, string>> expression = s => s.ToLowerInvariant();

			var oDataExpression = visitor.Translate(expression.Body);

			Assert.That(oDataExpression, Is.EqualTo("tolower(s)"));
		}

		[Test]
		public void Translate_TrimMethodCallExpression_TranslatesCorrectly()
		{
			var visitor = new ODataExpressionVisitor();

			Expression<Func<string, string>> expression = s => s.Trim();

			var oDataExpression = visitor.Translate(expression.Body);

			Assert.That(oDataExpression, Is.EqualTo("trim(s)"));
		}

		[Test]
		public void Translate_LengthPropertyAccessExpression_TranslatesCorrectly()
		{
			var visitor = new ODataExpressionVisitor();

			Expression<Func<string, int>> expression = s => s.Length;

			var oDataExpression = visitor.Translate(expression.Body);

			Assert.That(oDataExpression, Is.EqualTo("length(s)"));
		}

		[Test]
		public void Translate_YearPropertyAccessExpression_TranslatesCorrectly()
		{
			var visitor = new ODataExpressionVisitor();

			Expression<Func<DateTime, int>> expression = s => s.Year;

			var oDataExpression = visitor.Translate(expression.Body);

			Assert.That(oDataExpression, Is.EqualTo("year(s)"));
		}

		[Test]
		public void Translate_MonthPropertyAccessExpression_TranslatesCorrectly()
		{
			var visitor = new ODataExpressionVisitor();

			Expression<Func<DateTime, int>> expression = s => s.Month;

			var oDataExpression = visitor.Translate(expression.Body);

			Assert.That(oDataExpression, Is.EqualTo("month(s)"));
		}

		[Test]
		public void Translate_DayPropertyAccessExpression_TranslatesCorrectly()
		{
			var visitor = new ODataExpressionVisitor();

			Expression<Func<DateTime, int>> expression = s => s.Day;

			var oDataExpression = visitor.Translate(expression.Body);

			Assert.That(oDataExpression, Is.EqualTo("day(s)"));
		}

		[Test]
		public void Translate_HourPropertyAccessExpression_TranslatesCorrectly()
		{
			var visitor = new ODataExpressionVisitor();

			Expression<Func<DateTime, int>> expression = s => s.Hour;

			var oDataExpression = visitor.Translate(expression.Body);

			Assert.That(oDataExpression, Is.EqualTo("hour(s)"));
		}

		[Test]
		public void Translate_MinutePropertyAccessExpression_TranslatesCorrectly()
		{
			var visitor = new ODataExpressionVisitor();

			Expression<Func<DateTime, int>> expression = s => s.Minute;

			var oDataExpression = visitor.Translate(expression.Body);

			Assert.That(oDataExpression, Is.EqualTo("minute(s)"));
		}

		[Test]
		public void Translate_SecondPropertyAccessExpression_TranslatesCorrectly()
		{
			var visitor = new ODataExpressionVisitor();

			Expression<Func<DateTime, int>> expression = s => s.Second;

			var oDataExpression = visitor.Translate(expression.Body);

			Assert.That(oDataExpression, Is.EqualTo("second(s)"));
		}

		[Test]
		public void Translate_CeilingMethodCallExpression_TranslatesCorrectly()
		{
			var visitor = new ODataExpressionVisitor();

			Expression<Func<double, double>> expression = s => Math.Ceiling(s);

			var oDataExpression = visitor.Translate(expression.Body);

			Assert.That(oDataExpression, Is.EqualTo("ceiling(s)"));
		}

		[Test]
		public void Translate_FloorMethodCallExpression_TranslatesCorrectly()
		{
			var visitor = new ODataExpressionVisitor();

			Expression<Func<double, double>> expression = s => Math.Floor(s);

			var oDataExpression = visitor.Translate(expression.Body);

			Assert.That(oDataExpression, Is.EqualTo("floor(s)"));
		}

		[Test]
		public void Translate_RoundCallExpression_TranslatesCorrectly()
		{
			var visitor = new ODataExpressionVisitor();

			Expression<Func<double, double>> expression = s => Math.Round(s);

			var oDataExpression = visitor.Translate(expression.Body);

			Assert.That(oDataExpression, Is.EqualTo("round(s)"));
		}

		[Test]
		public void Translate_IndexOfMethodCallExpression_TranslatesCorrectly()
		{
			var visitor = new ODataExpressionVisitor();

			Expression<Func<string, int>> expression = s => s.IndexOf("hello");

			var oDataExpression = visitor.Translate(expression.Body);

			Assert.That(oDataExpression, Is.EqualTo("indexof(s, 'hello')"));
		}

		[Test]
		public void Translate_StartsWithMethodCallExpression_TranslatesCorrectly()
		{
			var visitor = new ODataExpressionVisitor();

			Expression<Func<string, bool>> expression = s => s.StartsWith("hello");

			var oDataExpression = visitor.Translate(expression.Body);

			Assert.That(oDataExpression, Is.EqualTo("startswith(s, 'hello')"));
		}

		[Test]
		public void Translate_EndsWithMethodCallExpression_TranslatesCorrectly()
		{
			var visitor = new ODataExpressionVisitor();

			Expression<Func<string, bool>> expression = s => s.EndsWith("hello");

			var oDataExpression = visitor.Translate(expression.Body);

			Assert.That(oDataExpression, Is.EqualTo("endswith(s, 'hello')"));
		}

		[Test]
		public void Translate_SubstringMethodCallExpression_TranslatesCorrectly()
		{
			var visitor = new ODataExpressionVisitor();

			Expression<Func<string, string>> expression = s => s.Substring(2);

			var oDataExpression = visitor.Translate(expression.Body);

			Assert.That(oDataExpression, Is.EqualTo("substring(s, 2)"));
		}

		[Test]
		public void Translate_ContainsMethodCallExpression_TranslatesCorrectly()
		{
			var visitor = new ODataExpressionVisitor();

			Expression<Func<string, bool>> expression = s => s.Contains("hello");

			var oDataExpression = visitor.Translate(expression.Body);

			Assert.That(oDataExpression, Is.EqualTo("substringof(s, 'hello')"));
		}
	}
}
