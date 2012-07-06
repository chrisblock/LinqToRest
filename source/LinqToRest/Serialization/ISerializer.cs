using System.Net.Http;

namespace LinqToRest.Serialization
{
	public interface ISerializer
	{
		string MediaType { get; }

		HttpContent Serialize(object objectToSerialize);

		T Deserialize<T>(HttpContent serializedObject);
	}
}
