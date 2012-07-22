using System;
using System.Net.Http;

namespace LinqToRest.Serialization
{
	public interface ISerializer
	{
		HttpContent Serialize<T>(T objectToSerialize);

		HttpContent Serialize(object objectToSerialize);

		T Deserialize<T>(HttpContent serializedObject);

		object Deserialize(Type serializedType, HttpContent serializedObject);
	}
}
