using LinqToRest.Serialization;
using LinqToRest.Serialization.Impl;

namespace LinqToRest.Server
{
	internal class ServerDependencyResolver : AbstractDependencyResolver
	{
		public ServerDependencyResolver()
		{
			Register<ISerializer, JsonSerializer>();
		}
	}
}
