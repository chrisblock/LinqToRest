using LinqToRest.OData.Literals;
using LinqToRest.OData.Literals.Impl;

using NUnit.Framework;

namespace LinqToRest.Tests.Literals
{
	[TestFixture]
	public class DoubleLiteralTests
	{
		private ILiteral _literal = new DoubleLiteral();

		[SetUp]
		public void TestSetUp()
		{
			_literal = new DoubleLiteral();
		}
	}
}
