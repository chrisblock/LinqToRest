using System.Collections.Generic;
using System.Linq;
using System.Net;

using Changes;

using LinqToRest.Client.Http;
using LinqToRest.Client.Linq;

namespace LinqToRest.Client
{
	public class Endpoint
	{
		private readonly IRestQueryableFactory _restQueryableFactory;
		private readonly IHttpService _httpService;
		private readonly IUriFactory _uriFactory;

		public Endpoint(IRestQueryableFactory restQueryableFactory, IHttpService httpService, IUriFactory uriFactory)
		{
			_restQueryableFactory = restQueryableFactory;
			_httpService = httpService;
			_uriFactory = uriFactory;
		}

		public IQueryable<T> Get<T>()
		{
			return _restQueryableFactory.Create<T>();
		}

		public HttpStatusCode Put<T>(object id, ChangeSet<T> item)
		{
			var uri = _uriFactory.CreateItemUri<T>(id);

			return _httpService.Put(uri, item);
		}

		public IEnumerable<HttpStatusCode> Post<T>(params T[] items)
		{
			return Post(items.AsEnumerable());
		}

		public IEnumerable<HttpStatusCode> Post<T>(IEnumerable<T> items)
		{
			return items.Select(Post);
		}

		public HttpStatusCode Post<T>(T item)
		{
			var uri = _uriFactory.GetCollectionUri<T>();

			return _httpService.Post(uri, item);
		}

		public HttpStatusCode Delete<T>(object id)
		{
			var uri = _uriFactory.CreateItemUri<T>(id);

			return _httpService.Delete(uri);
		}
	}
}
