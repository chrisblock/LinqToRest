using System;

namespace LinqToRest.Client
{
	public class UriFactory : IUriFactory
	{
		public Uri BaseUri { get; }

		public UriFactory(Uri baseUri)
		{
			BaseUri = baseUri;
		}

		public Uri GetItemUri(string resourceName, object id)
		{
			Uri result;

			if (Uri.TryCreate(BaseUri, $"{resourceName}/{id}", out result) == false)
			{
				throw new ArgumentException($"Unable to create item URI with base URL '{BaseUri}', resource name '{resourceName}' and id '{id}'.");
			}

			return result;
		}

		public Uri GetCollectionUri(string resourceName)
		{
			Uri result;

			if (Uri.TryCreate(BaseUri, $"{resourceName}/", out result) == false)
			{
				throw new ArgumentException($"Unable to create collection URI with base URL '{BaseUri}' and resource name '{resourceName}'.");
			}

			return result;
		}
	}
}
