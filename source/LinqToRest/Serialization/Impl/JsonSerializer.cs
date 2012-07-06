using System;
using System.Net.Http;
using System.Text;

using Newtonsoft.Json;

namespace LinqToRest.Serialization.Impl
{
	public class JsonSerializer : ISerializer
	{
		public string MediaType { get { return "application/json"; } }

		public HttpContent Serialize(object objectToSerialize)
		{
			var content = new StringContent(JsonConvert.SerializeObject(objectToSerialize), Encoding.UTF8, MediaType);

			return content;
		}

		public T Deserialize<T>(HttpContent serializedObject)
		{
			var task = serializedObject.ReadAsAsync<T>();

			task.Wait();

			if (task.IsFaulted || (task.Exception != null))
			{
				throw new ArgumentException(String.Format("Could not read the content as {0}", typeof (T)));
			}
			
			if (task.IsCanceled)
			{
				throw new ApplicationException("Deserialization task canceled.");
			}

			var result = task.Result;

			return result;
		}
	}
}
