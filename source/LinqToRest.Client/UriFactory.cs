using System;

namespace LinqToRest.Client
{
	public class UriFactory : IUriFactory
	{
		private readonly Uri _uri;

		public UriFactory(Uri uri)
		{
			_uri = uri;
		}

		public Uri CreateItemUri<T>(object id)
		{
			return CreateItemUri(typeof(T), id);
		}

		public Uri CreateItemUri(Type resourceType, object id)
		{
			return CreateItemUri(resourceType.Name, id);
		}

		public Uri CreateItemUri(string resourceName, object id)
		{
			Uri result;

			if (Uri.TryCreate(_uri, String.Format("{0}/{1}", resourceName, id), out result) == false)
			{
				throw new ArgumentException(String.Format("Unable to create item URI with base URL '{0}', resource name '{1}' and id '{2}'.", _uri, resourceName, id));
			}

			return result;
		}

		public Uri GetCollectionUri<T>()
		{
			return GetCollectionUri(typeof (T));
		}

		public Uri GetCollectionUri(Type resourceType)
		{
			return GetCollectionUri(resourceType.Name);
		}

		public Uri GetCollectionUri(string resourceName)
		{
			Uri result;

			if (Uri.TryCreate(_uri, String.Format("{0}/", resourceName), out result) == false)
			{
				throw new ArgumentException(String.Format("Unable to create collection URI with base URL '{0}' and resource name '{1}'.", _uri, resourceName));
			}

			return result;
		}
	}
}
