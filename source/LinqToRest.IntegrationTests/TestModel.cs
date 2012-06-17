namespace LinqToRest.IntegrationTests
{
	[ServiceUrl("http://localhost:9000/api/TestModel")]
	public class TestModel
	{
		public int Id { get; set; }
		public string TestProperty { get; set; }
	}
}
