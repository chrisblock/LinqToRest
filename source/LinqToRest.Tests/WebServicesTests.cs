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
			var queryable = (RestQueryable<TestObject>)WebServices.Find<TestObject>();

			var generatedUrl = queryable.ToList();

			Assert.That(generatedUrl, Is.EqualTo("http://localhost/api/TestObjects"));
		}

		[Test]
		public void Find_TestObjectSkipOne_GeneratesCorrectUrl()
		{
			var queryable = (RestQueryable<TestObject>)WebServices.Find<TestObject>().Skip(1);

			var generatedUrl = queryable.ToList();

			Assert.That(generatedUrl, Is.EqualTo("http://localhost/api/TestObjects"));
		}
	}
}
