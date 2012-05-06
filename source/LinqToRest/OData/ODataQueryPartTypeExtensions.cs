using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqToRest.OData
{
	public static class ODataQueryPartTypeExtensions
	{
		private static readonly IDictionary<ODataQueryPartType, string> TypeToParameterNames;
		private static readonly IDictionary<string, ODataQueryPartType> ParameterNameToType;

		static ODataQueryPartTypeExtensions()
		{
			var fields = Enum.GetNames(typeof (ODataQueryPartType))
				.Select(typeof(ODataQueryPartType).GetField)
				.ToList();

			TypeToParameterNames = fields
				.Where(x => x.GetCustomAttributes<UrlParameterAttribute>().Any())
				.ToDictionary(key => (ODataQueryPartType)key.GetValue(null), value => value.GetCustomAttributes<UrlParameterAttribute>().Single().Name);

			ParameterNameToType = TypeToParameterNames.ToDictionary(key => key.Value, value => value.Key);
		}

		public static string GetUrlParameterName(this ODataQueryPartType type)
		{
			string result;

			if (TypeToParameterNames.TryGetValue(type, out result) == false)
			{
				throw new ArgumentException(String.Format("ODataQueryPartType '{0}' does not have a url parameter associated with it.", type));
			}

			return result;
		}

		public static ODataQueryPartType GetFromUrlParameterName(this string parameterName)
		{
			ODataQueryPartType result;

			if (ParameterNameToType.TryGetValue(parameterName, out result) == false)
			{
				throw new ArgumentException(String.Format("URL parameter name '{0}' does not have an ODataQueryPartType associated with it.", parameterName));
			}

			return result;
		}
	}
}
