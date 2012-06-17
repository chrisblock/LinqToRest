using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using LinqToRest.Server.WebApi;

using TestWebApiService.Models;

namespace TestWebApiService.Controllers
{
	public class TestModelController : ApiController
	{
		private static readonly ICollection<TestModel> Items = new List<TestModel>
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

		// GET /api/values
		[ODataQuery]
		public HttpResponseMessage Get()
		{
			return Request.CreateResponse(HttpStatusCode.OK, Items.AsQueryable());
		}

		// GET /api/values/5
		public TestModel Get(int id)
		{
			return Items.SingleOrDefault(x => x.Id == id);
		}

		// POST /api/values
		public void Post(TestModel value)
		{
			Items.Add(value);
		}

		// PUT /api/values/5
		public void Put(int id, string value)
		{
			var item = Items.SingleOrDefault(x => x.Id == id);

			if (item != null)
			{
				item.TestProperty = value;
			}
		}

		// DELETE /api/values/5
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