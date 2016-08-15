using System;
using System.Net.Http;
using System.Net.Http.Formatting;

using Newtonsoft.Json;

namespace LinqToRest.Client.Serialization.Impl
{
	public class JsonSerializer : ISerializer
	{
		private static JsonSerializerSettings DefaultSerializerSettings => new JsonSerializerSettings
		{
			NullValueHandling = NullValueHandling.Ignore,
			DateFormatHandling = DateFormatHandling.IsoDateFormat
		};

		private JsonMediaTypeFormatter JsonMediaTypeFormatter { get; }

		public JsonSerializer() : this(DefaultSerializerSettings)
		{
		}

		public JsonSerializer(JsonSerializerSettings settings)
		{
			JsonMediaTypeFormatter = new JsonMediaTypeFormatter
			{
				SerializerSettings = settings
			};
		}

		public HttpContent Serialize<T>(T objectToSerialize)
		{
			var content = new ObjectContent<T>(objectToSerialize, JsonMediaTypeFormatter);

			return content;
		}

		public T Deserialize<T>(HttpContent serializedObject)
		{
			var task = serializedObject.ReadAsAsync<T>(new[] { JsonMediaTypeFormatter });

			task.Wait();

			if (task.IsFaulted || (task.Exception != null))
			{
				throw new ArgumentException($"Could not read the content as type '{typeof (T)}'.");
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
