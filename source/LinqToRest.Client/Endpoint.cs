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

		public Resource<T> GetResource<T>()
		{
			var type = typeof (T);

			return GetResource<T>(type.Name);
		}

		public Resource<T> GetResource<T>(string name)
		{
			return new Resource<T>(name, _restQueryableFactory, _httpService, _uriFactory);
		}
	}
}
