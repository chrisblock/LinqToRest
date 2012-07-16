using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading;

namespace LinqToRest
{
	public static class AnonymousTypeManager
	{
		private static readonly object TypeLocker = new object();
		private static readonly ConcurrentDictionary<string, Type> Types = new ConcurrentDictionary<string, Type>();

		private static readonly Lazy<AssemblyName> LazyAssemblyName = new Lazy<AssemblyName>(() => new AssemblyName("DynamicLinqTypes"), LazyThreadSafetyMode.ExecutionAndPublication);
		private static AssemblyName AssemblyName { get { return LazyAssemblyName.Value; } }

		private static readonly Lazy<AssemblyBuilder> LazyAssemblyBuilder = new Lazy<AssemblyBuilder>(() => Thread.GetDomain().DefineDynamicAssembly(AssemblyName, AssemblyBuilderAccess.Run), LazyThreadSafetyMode.ExecutionAndPublication);
		private static AssemblyBuilder AssemblyBuilder { get { return LazyAssemblyBuilder.Value; } }

		private static readonly Lazy<ModuleBuilder> LazyModuleBuilder = new Lazy<ModuleBuilder>(() => AssemblyBuilder.DefineDynamicModule(AssemblyName.Name), LazyThreadSafetyMode.ExecutionAndPublication);
		private static ModuleBuilder ModuleBuilder { get { return LazyModuleBuilder.Value; } }

		public static Type BuildType(IEnumerable<Property> members)
		{
			if (members == null)
			{
				throw new ArgumentNullException("members", "Cannot build type for the null set of properties.");
			}

			var properties = members.ToList();

			if (properties.Any() == false)
			{
				throw new ArgumentException("Cannot build type for the empty set of properties.");
			}

			var typeName = String.Join("_", properties.Select(x => x.Name).OrderBy(x => x));

			if (Types.ContainsKey(typeName) == false)
			{
				lock (TypeLocker)
				{
					if (Types.ContainsKey(typeName) == false)
					{
						var typeBuilder = ModuleBuilder.DefineType(typeName, TypeAttributes.Public | TypeAttributes.Class | TypeAttributes.Serializable);

						foreach (var m in properties)
						{
							typeBuilder.DefineField(m.Name, m.Type, FieldAttributes.Public);
						}

						var newType = typeBuilder.CreateType();
						Types.TryAdd(typeName, newType);
					}
				}
			}

			return Types[typeName];
		}
	}
}
