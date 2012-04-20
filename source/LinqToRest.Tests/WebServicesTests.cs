using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

using NUnit.Framework;

namespace LinqToRest.Tests
{
	[ServiceUrl("http://localhost/api/TestObjects")]
	public class TestObject
	{
		public string TestProperty { get; set; }
	}

	[TestFixture]
	public class WebServicesTests
	{
		[Test]
		public void Find_TypeWithoutServiceUrlAttributeSpecified_ThrowsArgumentException()
		{
			Assert.That(() => WebServices.Find<int>(), Throws.ArgumentException);
		}

		[Test]
		public void Find_TestObject_GeneratesCorrectUrl()
		{
			var queryable = WebServices.Find<TestObject>();

			var generatedUrl = queryable.ToList();

			Assert.That(generatedUrl, Is.EqualTo("http://localhost/api/TestObjects"));
		}

		[Test]
		public void Find_TestObjectSkipOne_GeneratesCorrectUrl()
		{
			var skipConstant = 3;
			var queryable = WebServices.Find<TestObject>().Skip(skipConstant);

			var generatedUrl = queryable.ToList();

			Assert.That(generatedUrl, Is.EqualTo("http://localhost/api/TestObjects"));
		}

		[Test]
		public void Find_TestObjectOrderBy_GeneratesCorrectUrl()
		{
			var queryable = WebServices.Find<TestObject>().Where(x => x.TestProperty != null).OrderBy(x => x.TestProperty).Select(x => x.TestProperty);

			var generatedUrl = queryable.ToList();

			Assert.That(generatedUrl, Is.EqualTo("http://localhost/api/TestObjects"));
		}
	}
}
