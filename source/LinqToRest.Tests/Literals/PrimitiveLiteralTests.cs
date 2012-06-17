using LinqToRest.OData.Literals;
using LinqToRest.OData.Literals.Impl;

using NUnit.Framework;

namespace LinqToRest.Tests.Literals
{
	[TestFixture]
	public class PrimitiveLiteralTests
	{
		private ILiteral _literal = new PrimitiveLiteral();

		[SetUp]
		public void TestSetUp()
		{
			_literal = new PrimitiveLiteral();
		}
	}
}
