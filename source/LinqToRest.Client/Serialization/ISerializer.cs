using System.Net.Http;

namespace LinqToRest.Client.Serialization
{
	public interface ISerializer
	{
		HttpContent Serialize<T>(T objectToSerialize);

		T Deserialize<T>(HttpContent serializedObject);
	}
}
