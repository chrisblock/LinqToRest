using System;

namespace WebApi.SelfHost
{
	public static class AppDomainExtensions
	{
		public static T CreateInstanceAndUnwrap<T>(this AppDomain appDomain)
			where T : MarshalByRefObject
		{
			var type = typeof (T);

			var assembly = type.Assembly;

			var typeName = type.FullName;

			return (T) appDomain.CreateInstanceAndUnwrap(assembly.FullName, typeName);
		}

		public static void LoadAssemblyContainingType<T>(this AppDomain appDomain)
		{
			var assembly = typeof (T).Assembly;
			var assemblyName = assembly.GetName();

			appDomain.Load(assemblyName);
		}
	}
}
