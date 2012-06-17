using LinqToRest.OData.Literals;
using LinqToRest.OData.Literals.Impl;

using NUnit.Framework;

namespace LinqToRest.Tests.Literals
{
	[TestFixture]
	public class FloatLiteralTests
	{
		private ILiteral _literal = new FloatLiteral();

		[SetUp]
		public void TestSetUp()
		{
			_literal = new FloatLiteral();
		}
	}
}
