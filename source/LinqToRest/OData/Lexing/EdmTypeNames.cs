using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqToRest.OData.Lexing
{
	public static class EdmTypeNames
	{
		private static readonly IDictionary<Type, string> TypeToEdmName;
		private static readonly IDictionary<string, Type> EdmNameToType;

		static EdmTypeNames()
		{
			TypeToEdmName = new Dictionary<Type, string>
			{
				// { typeof (binary), "edm.binary" },
				{ typeof (bool), "edm.boolean" },
				{ typeof (byte), "edm.byte" },
				{ typeof (DateTime), "edm.datetime" },
				{ typeof (decimal), "edm.decimal" },
				{ typeof (double), "edm.double" },
				{ typeof (float), "edm.float" },
				{ typeof (Guid), "edm.guid" },
				{ typeof (short), "edm.int16" },
				{ typeof (int), "edm.int32" },
				{ typeof (long), "edm.int64" },
				{ typeof (sbyte), "edm.sbyte" },
				{ typeof (string), "edm.string" },
				{ typeof (TimeSpan), "edm.time" },
				{ typeof (DateTimeOffset), "edm.datetimeoffset" }
				// { typeof (Stream), "edm.stream" }
			};

			EdmNameToType = TypeToEdmName.ToDictionary(x => x.Value, x => x.Key);
			EdmNameToType.Add("edm.single", typeof (float));
		}

		public static Type Lookup(string edmPrimitiveName)
		{
			Type result;

			EdmNameToType.TryGetValue(edmPrimitiveName.ToLowerInvariant(), out result);

			return result;
		}

		public static string Lookup(Type type)
		{
			string result;

			TypeToEdmName.TryGetValue(type, out result);

			return result;
		}
	}
}
