using Newtonsoft.Json;

namespace LinqToRest.Serialization.Impl
{
	public class JsonSerializer : ISerializer
	{
		public string Serialize(object objectToSerialize)
		{
			return JsonConvert.SerializeObject(objectToSerialize);
		}

		public T Deserialize<T>(string serializedObject)
		{
			return JsonConvert.DeserializeObject<T>(serializedObject);
		}
	}
}
