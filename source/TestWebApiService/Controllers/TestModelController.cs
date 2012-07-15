using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Changes;

using LinqToRest.Server.WebApi;

using TestWebApiService.Models;

namespace TestWebApiService.Controllers
{
	public class TestModelController : ApiController
	{
		private static readonly ICollection<TestModel> Items;
		
		static TestModelController()
		{
			Items = new List<TestModel>
			{
				new TestModel
				{
					Id = 1,
					TestProperty = "1"
				},
				new TestModel
				{
					Id = 2,
					TestProperty = "2"
				},
				new TestModel
				{
					Id = 3,
					TestProperty = "3"
				},
				new TestModel
				{
					Id = 4,
					TestProperty = "4"
				}
			};
		}

		[ODataQuery]
		public HttpResponseMessage Get()
		{
			return Request.CreateResponse(HttpStatusCode.OK, Items.AsQueryable());
		}

		public TestModel Get(int id)
		{
			return Items.SingleOrDefault(x => x.Id == id);
		}

		public void Post(TestModel value)
		{
			Items.Add(value);
		}

		public void Put(int id, ChangeSet<TestModel> changeSet)
		{
			var item = Items.SingleOrDefault(x => x.Id == id);

			if (item != null)
			{
				changeSet.ApplyChanges(ref item);
			}
			else
			{
				// TODO: return 404 or something
			}
		}

		public void Delete(int id)
		{
			var item = Items.SingleOrDefault(x => x.Id == id);

			if (item != null)
			{
				Items.Remove(item);
			}
		}
	}
}
