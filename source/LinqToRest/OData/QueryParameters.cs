using System;
using System.Collections.Generic;

namespace LinqToRest.OData
{
	public static class QueryParameters
	{
		private static readonly IDictionary<ODataQueryPartType, string> ParameterTypeStrings;

		static QueryParameters()
		{
			ParameterTypeStrings = new Dictionary<ODataQueryPartType, string>
			{
				{ODataQueryPartType.Expand, "$expand"},
				{ODataQueryPartType.Filter, "$filter"},
				{ODataQueryPartType.Format, "$format"},
				{ODataQueryPartType.OrderBy, "$orderby"},
				{ODataQueryPartType.Select, "$select"},
				{ODataQueryPartType.Skip, "$skip"},
				{ODataQueryPartType.SkipToken, "$skiptoken"},
				{ODataQueryPartType.Top, "$top"}
			};
		}

		public static string Expand { get { return "$expand"; } }
		public static string Filter { get { return "$filter"; } }
		public static string Format { get { return "$format"; } }
		public static string OrderBy { get { return "$orderby"; } }
		public static string Select { get { return "$select"; } }
		public static string Skip { get { return "$skip"; } }
		public static string SkipToken { get { return "$skiptoken"; } }
		public static string Top { get { return "$top"; } }

		public static string GetString(ODataQueryPartType parameterType)
		{
			string result;

			if (ParameterTypeStrings.TryGetValue(parameterType, out result) == false)
			{
				throw new ArgumentOutOfRangeException("parameterType", String.Format("'{0}' is not recognized or is not a parameter type.", parameterType));
			}

			return result;
		}
	}
}
