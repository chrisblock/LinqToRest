using StructureMap;

using TestWebApiService.Controllers;

namespace TestWebApiService
{
	public class TestWebApiServiceRegistry : Registry
	{
		public TestWebApiServiceRegistry()
		{
			Scan(scan =>
			{
				scan.AssemblyContainingType<TestWebApiServiceRegistry>();

				scan.WithDefaultConventions();
				scan.SingleImplementationsOfInterface();
			});

			For<TestObjectRepository>()
				.Singleton();
		}
	}
}
