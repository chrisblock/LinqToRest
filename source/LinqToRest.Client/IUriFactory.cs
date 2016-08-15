using System;

namespace LinqToRest.Client
{
	public interface IUriFactory
	{
		Uri BaseUri { get; }
		Uri GetItemUri(string resourceName, object id);
		Uri GetCollectionUri(string resourceName);
	}
}
