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
		public void Find_WhereIdEqualsThree_ReturnsListWithSingleResult()
		{
			var result = WebServices.Find<TestModel>().Where(x => x.Id == 3).ToList();

			Assert.That(result.Count, Is.EqualTo(1));
			Assert.That(result.Single().Id, Is.EqualTo(3));
			Assert.That(result.Single().TestProperty, Is.EqualTo("3"));
		}

		[Test]
		public void Find_WhereIdModTwoEqualsZero_ReturnsListWithTwoResults()
		{
			var result = WebServices.Find<TestModel>().Where(x => x.Id % 2 == 0).ToList();

			Assert.That(result.Count, Is.EqualTo(2));
			Assert.That(result.ElementAt(0).Id, Is.EqualTo(2));
			Assert.That(result.ElementAt(0).TestProperty, Is.EqualTo("2"));
			Assert.That(result.ElementAt(1).Id, Is.EqualTo(4));
			Assert.That(result.ElementAt(1).TestProperty, Is.EqualTo("4"));
		}
	}
}
