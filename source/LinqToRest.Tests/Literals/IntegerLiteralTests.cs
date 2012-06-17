using LinqToRest.OData.Literals;
using LinqToRest.OData.Literals.Impl;

using NUnit.Framework;

namespace LinqToRest.Tests.Literals
{
	[TestFixture]
	public class IntegerLiteralTests
	{
		private ILiteral _literal = new IntegerLiteral();

		[SetUp]
		public void TestSetUp()
		{
			_literal = new IntegerLiteral();
		}
	}
}
