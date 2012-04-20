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

			Assert.That(oDataExpression, Is.EqualTo("((s eq 'hello') and (Length lt 4))"));
		}

		[Test]
		public void Translate_OrExpression_TranslatesCorrectly()
		{
			var visitor = new ODataExpressionVisitor();

			Expression<Func<string, bool>> expression = s => s == "hello" || s.Length < 4;

			var oDataExpression = visitor.Translate(expression.Body);

			Assert.That(oDataExpression, Is.EqualTo("((s eq 'hello') or (Length lt 4))"));
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
		public void Translate_SubtractionExpression_TranslatesCorrectly()
		{
			var visitor = new ODataExpressionVisitor();

			Expression<Func<int, int>> expression = s => s - 4;

			var oDataExpression = visitor.Translate(expression.Body);

			Assert.That(oDataExpression, Is.EqualTo("(s sub 4)"));
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

			Assert.That(oDataExpression, Is.EqualTo("((- s) gt 3)"));
		}

		[Test]
		public void Translate_NotExpression_TranslatesCorrectly()
		{
			var visitor = new ODataExpressionVisitor();

			Expression<Func<bool, bool>> expression = s => !s;

			var oDataExpression = visitor.Translate(expression.Body);

			Assert.That(oDataExpression, Is.EqualTo("(not s)"));
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
	}
}
