using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqToRest.OData.Filters
{
	public static class FunctionEnumExtensions
	{
		private static readonly IDictionary<Function, string> FunctionToDotNetMethodName;
		private static readonly IDictionary<Function, string> FunctionToODataQueryMethodName;
		private static readonly IDictionary<string, Function> DotNetMethodNameToFunction;
		private static readonly IDictionary<string, Function> ODataQueryMethodNameToFunction;

		static FunctionEnumExtensions()
		{
			var fields = Enum.GetNames(typeof (Function))
				.Select(typeof(Function).GetField)
				.ToList();

			FunctionToDotNetMethodName = fields
				.Where(x => x.GetCustomAttributes<DotNetMethodAttribute>().Any())
				.ToDictionary(key => (Function)key.GetValue(null), value => value.GetCustomAttributes<DotNetMethodAttribute>().Single().Name);

			FunctionToODataQueryMethodName = fields
				.Where(x => x.GetCustomAttributes<ODataQueryMethodAttribute>().Any())
				.ToDictionary(key => (Function)key.GetValue(null), value => value.GetCustomAttributes<ODataQueryMethodAttribute>().Single().Name);

			DotNetMethodNameToFunction = FunctionToDotNetMethodName.ToDictionary(key => key.Value, value => value.Key);

			ODataQueryMethodNameToFunction = FunctionToODataQueryMethodName.ToDictionary(key => key.Value, value => value.Key);
		}

		public static string GetDotNetMethodName(this Function function)
		{
			string methodName;

			if (FunctionToDotNetMethodName.TryGetValue(function, out methodName) == false)
			{
				throw new ArgumentException(String.Format("No .NET method name defined for '{0}'.", function));
			}

			return methodName;
		}

		public static Function GetFromDotNetMethodName(this string dotNetMethodName)
		{
			Function result;

			if (DotNetMethodNameToFunction.TryGetValue(dotNetMethodName, out result) == false)
			{
				throw new ArgumentException(String.Format("Could not find enum value with .NET method name '{0}'.", dotNetMethodName));
			}

			return result;
		}

		public static string GetODataQueryMethodName(this Function function)
		{
			string methodName;

			if (FunctionToODataQueryMethodName.TryGetValue(function, out methodName) == false)
			{
				throw new ArgumentException(String.Format("No ODataQuery method name defined for '{0}'.", function));
			}

			return methodName;
		}

		public static Function GetFromODataQueryMethodName(this string oDataQueryMethodName)
		{
			Function result;

			if (ODataQueryMethodNameToFunction.TryGetValue(oDataQueryMethodName, out result) == false)
			{
				throw new ArgumentException(String.Format("Could not find enum value with OData Query method name '{0}'.", oDataQueryMethodName));
			}

			return result;
		}
	}
}
