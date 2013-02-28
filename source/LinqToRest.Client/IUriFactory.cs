using System;

namespace LinqToRest.Client
{
	public interface IUriFactory
	{
		Uri CreateItemUri<T>(object id);
		Uri CreateItemUri(Type resourceType, object id);
		Uri CreateItemUri(string resourceName, object id);
		Uri GetCollectionUri<T>();
		Uri GetCollectionUri(Type resourceType);
		Uri GetCollectionUri(string resourceName);
	}
}
