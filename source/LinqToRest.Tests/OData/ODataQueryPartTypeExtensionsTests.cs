// ReSharper disable InconsistentNaming

using LinqToRest.OData;

using NUnit.Framework;

namespace LinqToRest.Tests.OData
{
	[TestFixture]
	public class ODataQueryPartTypeExtensionsTests
	{
		[Test]
		public void GetUrlParameterName_UnknownQueryPartType_ThrowsException()
		{
			Assert.That(() => ODataQueryPartType.Unknown.GetUrlParameterName(), Throws.ArgumentException);
		}

		[Test]
		public void GetFromUrlParameterName_StringNotContainingQueryPartType_ThrowsException()
		{
			Assert.That(() => "Hello, World.".GetFromUrlParameterName(), Throws.ArgumentException);
		}
	}
}
