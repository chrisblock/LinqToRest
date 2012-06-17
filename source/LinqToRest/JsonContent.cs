using System.Net.Http;
using System.Text;

namespace LinqToRest
{
	public class JsonContent : StringContent
	{
		public JsonContent(string content) : base(content, Encoding.UTF8, "application/json")
		{
		}

		public JsonContent(string content, Encoding encoding) : base(content, encoding, "application/json")
		{
		}
	}
}
