using System;

namespace LinqToRest
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	public class ServiceUrlAttribute : Attribute
	{
		public string Url { get; private set; }

		// TODO: add defaulted Id property name argument??
		public ServiceUrlAttribute(string url) //, id = "Id");
		{
			Url = url;
		}

		public Uri GetCollectionUri()
		{
			return new Uri(Url);
		}

		public Uri GetItemUri(object itemId)
		{
			var id = String.Format("{0}", itemId);

			if ((itemId == null) || String.IsNullOrWhiteSpace(id))
			{
				throw new ArgumentException(String.Format("Cannot create item URI with malformed item id '{0}'.", itemId));
			}

			var baseUri = GetCollectionUri();

			Uri result;

			if (Uri.TryCreate(baseUri, id, out result) == false)
			{
				throw new ArgumentException(String.Format("Unable to create item URI with base URL '{0}' and id '{1}'.", Url, itemId));
			}

			return result;
		}
	}
}
