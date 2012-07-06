// ReSharper disable InconsistentNaming

using LinqToRest.OData;

using NUnit.Framework;

namespace LinqToRest.Tests.OData
{
	[TestFixture]
	public class ODataOrderingTests
	{
		[Test]
		public void Constructor_NullAscending_ThrowsArgumentException()
		{
			Assert.That(() => new ODataOrdering(null, ODataOrderingDirection.Asc), Throws.ArgumentException);
		}

		[Test]
		public void Constructor_WhiteSpaceAscending_ThrowsArgumentException()
		{
			Assert.That(() => new ODataOrdering("  \t\r\n", ODataOrderingDirection.Asc), Throws.ArgumentException);
		}

		[Test]
		public void QueryPartType_ReturnsOrdering()
		{
			var ordering = new ODataOrdering("ColumnName", ODataOrderingDirection.Asc);

			Assert.That(ordering.QueryPartType, Is.EqualTo(ODataQueryPartType.Ordering));
		}

		[Test]
		public void ToString_ColumnNameAscending_ReturnsCorrectString()
		{
			var ordering = new ODataOrdering("ColumnName", ODataOrderingDirection.Asc);

			Assert.That(ordering.ToString(), Is.EqualTo("ColumnName asc"));
		}

		[Test]
		public void ToString_ColumnNameDescending_ReturnsCorrectString()
		{
			var ordering = new ODataOrdering("ColumnName", ODataOrderingDirection.Desc);

			Assert.That(ordering.ToString(), Is.EqualTo("ColumnName desc"));
		}
	}
}
