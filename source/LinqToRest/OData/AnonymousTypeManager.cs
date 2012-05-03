using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading;

namespace LinqToRest.OData
{
	public static class AnonymousTypeManager
	{
		private static readonly AssemblyName AssemblyName;

		private static readonly object TypeLocker = new object();
		private static readonly ConcurrentDictionary<string, Type> Types;

		static AnonymousTypeManager()
		{
			AssemblyName = new AssemblyName { Name = "DynamicLinqTypes" };
			Types = new ConcurrentDictionary<string, Type>();
		}

		private static readonly Lazy<ModuleBuilder> _moduleBuilder = new Lazy<ModuleBuilder>(() => Thread.GetDomain().DefineDynamicAssembly(AssemblyName, AssemblyBuilderAccess.Run).DefineDynamicModule(AssemblyName.Name), LazyThreadSafetyMode.ExecutionAndPublication);

		private static ModuleBuilder ModuleBuilder
		{
			get
			{
				return _moduleBuilder.Value;
			}
		}

		public static Type BuildType(IEnumerable<PropertyInfo> members)
		{
			var typeName = String.Join("_", members.Select(x => x.Name).OrderBy(x => x));

			if (Types.ContainsKey(typeName) == false)
			{
				lock (TypeLocker)
				{
					if (Types.ContainsKey(typeName))
					{
						var typeBuilder = ModuleBuilder.DefineType(typeName, TypeAttributes.Public | TypeAttributes.Class | TypeAttributes.Serializable);

						foreach (var m in members)
						{
							// TODO: make these things properties??
							typeBuilder.DefineProperty(m.Name, PropertyAttributes.None, CallingConventions.HasThis, m.PropertyType, Type.EmptyTypes);
							//typeBuilder.DefineField(m.Name, m.PropertyType, FieldAttributes.Public);
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