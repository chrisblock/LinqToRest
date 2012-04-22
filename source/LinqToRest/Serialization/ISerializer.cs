namespace LinqToRest.Serialization
{
	public interface ISerializer
	{
		string Serialize(object objectToSerialize);
		T Deserialize<T>(string serializedObject);
	}
}
