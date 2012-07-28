using System;
using System.Net.Http;
using System.Net.Http.Formatting;

namespace LinqToRest.Serialization.Impl
{
	public class JsonSerializer : ISerializer
	{
		public HttpContent Serialize<T>(T objectToSerialize)
		{
			var content = new ObjectContent<T>(objectToSerialize, new JsonMediaTypeFormatter());

			return content;
		}

		public T Deserialize<T>(HttpContent serializedObject)
		{
			var task = serializedObject.ReadAsAsync<T>();

			task.Wait();

			if (task.IsFaulted || (task.Exception != null))
			{
				throw new ArgumentException(String.Format("Could not read the content as type '{0}'.", typeof (T)));
			}
			
			if (task.IsCanceled)
			{
				throw new ApplicationException("Deserialization task was canceled.");
			}

			var result = task.Result;

			return result;
		}
	}
}
