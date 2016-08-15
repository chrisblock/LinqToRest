using System.Collections.Generic;
using System.Linq;
using System.Net;

using Changes;

using LinqToRest.Client.Http;
using LinqToRest.Client.Linq;

namespace LinqToRest.Client
{
	public class Resource<T>
	{
		private readonly string _name;
		private readonly IRestQueryableFactory _restQueryableFactory;
		private readonly IHttpService _httpService;
		private readonly IUriFactory _uriFactory;

		public Resource(string name, IRestQueryableFactory restQueryableFactory, IHttpService httpService, IUriFactory uriFactory)
		{
			_name = name;
			_restQueryableFactory = restQueryableFactory;
			_httpService = httpService;
			_uriFactory = uriFactory;
		}

		public IQueryable<T> Get()
		{
			var uri = _uriFactory.GetCollectionUri(_name);

			var result = _restQueryableFactory.Create<T>(uri);

			return result;
		}

		public T Get<TId>(TId id)
		{
			var uri = _uriFactory.GetItemUri(_name, id);

			var result = _httpService.Get<T>(uri);

			return result;
		}

		public HttpStatusCode Put<TId>(TId id, ChangeSet<T> delta)
		{
			var uri = _uriFactory.GetItemUri(_name, id);

			var result = _httpService.Put(uri, delta);

			return result;
		}

		public HttpStatusCode Post(T item)
		{
			var uri = _uriFactory.GetCollectionUri(_name);

			var result = _httpService.Post(uri, item);

			return result;
		}

		public IEnumerable<HttpStatusCode> Post(params T[] items)
		{
			return Post(items.AsEnumerable());
		}

		public IEnumerable<HttpStatusCode> Post(IEnumerable<T> items)
		{
			var uri = _uriFactory.GetCollectionUri(_name);

			var result = items.Select(x => _httpService.Post(uri, x));

			return result;
		}

		public HttpStatusCode Delete<TId>(TId id)
		{
			var uri = _uriFactory.GetItemUri(_name, id);

			var result = _httpService.Delete(uri);

			return result;
		}
	}
}
