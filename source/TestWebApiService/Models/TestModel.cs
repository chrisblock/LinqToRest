using LinqToRest;

namespace TestWebApiService.Models
{
	[ServiceUrl("http://localhost:6789/api/TestModel")]
	public class TestModel
	{
		public int Id { get; set; }
		public string TestProperty { get; set; }
	}
}
