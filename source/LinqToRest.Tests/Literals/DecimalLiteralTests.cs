using LinqToRest.OData.Literals;
using LinqToRest.OData.Literals.Impl;

using NUnit.Framework;

namespace LinqToRest.Tests.Literals
{
	[TestFixture]
	public class DecimalLiteralTests
	{
		private ILiteral _literal = new DecimalLiteral();

		[SetUp]
		public void TestSetUp()
		{
			_literal = new DecimalLiteral();
		}
	}
}
