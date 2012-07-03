namespace LinqToRest.Serialization
{
	public interface ISerializer
	{
		string MediaType { get; }

		// TODO: somehow provide an interface for serializing content to upload in a way that sets the Content-Type
		//       (may not be necessary with the RC due to the content negotiation being moved to the Response object out of the HttpContent object
		string Serialize(object objectToSerialize);
		//HttpContent Serialize(object objectToSerialize);
		T Deserialize<T>(string serializedObject);
	}
}
