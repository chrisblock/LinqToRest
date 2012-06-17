using LinqToRest.Serialization;
using LinqToRest.Serialization.Impl;

using StructureMap.Configuration.DSL;

namespace LinqToRest.StructureMap
{
	public class LinqToRestRegistry : Registry
	{
		public LinqToRestRegistry()
		{
			Scan(scan =>
			{
				scan.AssemblyContainingType<IDependencyResolver>();
				scan.WithDefaultConventions();
			});

			For<IHttpService>()
				.Singleton()
				.Use<HttpService>();

			For<ISerializer>()
				.Singleton()
				.Use<JsonSerializer>();
		}
	}
}
