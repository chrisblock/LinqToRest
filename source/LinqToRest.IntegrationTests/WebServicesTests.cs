using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

namespace LinqToRest.IntegrationTests
{
	[ServiceUrl("http://localhost:9000/api/TestModel")]
	public class TestModel
	{
		public int Id { get; set; }
		public string TestProperty { get; set; }
	}

	[TestFixture]
	public class WebServicesTests
	{
		[Test]
		public void Test()
		{
			var result = WebServices.Find<TestModel>().Where(x => x.Id == 3).ToList();

			Assert.That(result.Count, Is.EqualTo(1));
			Assert.That(result.Single().Id, Is.EqualTo(3));
			Assert.That(result.Single().TestProperty, Is.EqualTo("3"));
		}
	}
}
