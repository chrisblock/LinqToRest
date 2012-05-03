using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LinqToRest.OData.Filters
{
	public static class ODataQueryFilterExpressionOperatorEnumExtensions
	{
		private static readonly IDictionary<ODataQueryFilterExpressionOperator, ExpressionType> ODataQueryFilterExpressionOperatorToDotNetExpressionType;
		private static readonly IDictionary<ODataQueryFilterExpressionOperator, string> ODataQueryFilterExpressionOperatorToODataQueryOperatorString;
		private static readonly IDictionary<ExpressionType, ODataQueryFilterExpressionOperator> DotNetExpressionTypeToODataQueryFilterExpressionOperator;
		private static readonly IDictionary<string, ODataQueryFilterExpressionOperator> ODataQueryMethodNameToFunction;

		static ODataQueryFilterExpressionOperatorEnumExtensions()
		{
			var fields = Enum.GetNames(typeof(ODataQueryFilterExpressionOperator))
				.Select(typeof(ODataQueryFilterExpressionOperator).GetField)
				.ToList();

			ODataQueryFilterExpressionOperatorToDotNetExpressionType = fields
				.Where(x => x.GetCustomAttributes<DotNetOperatorAttribute>().Any())
				.ToDictionary(key => (ODataQueryFilterExpressionOperator)key.GetValue(null), value => value.GetCustomAttributes<DotNetOperatorAttribute>().Single().ExpressionType);

			ODataQueryFilterExpressionOperatorToODataQueryOperatorString = fields
				.Where(x => x.GetCustomAttributes<ODataQueryOperatorAttribute>().Any())
				.ToDictionary(key => (ODataQueryFilterExpressionOperator)key.GetValue(null), value => value.GetCustomAttributes<ODataQueryOperatorAttribute>().Single().Value);

			DotNetExpressionTypeToODataQueryFilterExpressionOperator = ODataQueryFilterExpressionOperatorToDotNetExpressionType.ToDictionary(key => key.Value, value => value.Key);

			ODataQueryMethodNameToFunction = ODataQueryFilterExpressionOperatorToODataQueryOperatorString.ToDictionary(key => key.Value, value => value.Key);
		}

		public static ExpressionType GetDotNetExpressionType(this ODataQueryFilterExpressionOperator oDataQueryOperator)
		{
			ExpressionType expressionType;

			if (ODataQueryFilterExpressionOperatorToDotNetExpressionType.TryGetValue(oDataQueryOperator, out expressionType) == false)
			{
				throw new ArgumentException(String.Format("No .NET ExpressionType defined for '{0}'.", oDataQueryOperator));
			}

			return expressionType;
		}

		public static ODataQueryFilterExpressionOperator GetFromDotNetExpressionType(this ExpressionType expressionType)
		{
			ODataQueryFilterExpressionOperator result;

			if (DotNetExpressionTypeToODataQueryFilterExpressionOperator.TryGetValue(expressionType, out result) == false)
			{
				throw new ArgumentException(String.Format("Could not find enum value with .NET ExpressionType '{0}'.", expressionType));
			}

			return result;
		}

		public static string GetODataQueryOperatorString(this ODataQueryFilterExpressionOperator oDataQueryOperator)
		{
			string result;

			if (ODataQueryFilterExpressionOperatorToODataQueryOperatorString.TryGetValue(oDataQueryOperator, out result) == false)
			{
				throw new ArgumentException(String.Format("No ODataQuery operator name defined for '{0}'.", oDataQueryOperator));
			}

			return result;
		}

		public static ODataQueryFilterExpressionOperator GetFromODataQueryOperatorString(this string oDataQueryOperatorString)
		{
			ODataQueryFilterExpressionOperator result;

			if (ODataQueryMethodNameToFunction.TryGetValue(oDataQueryOperatorString, out result) == false)
			{
				throw new ArgumentException(String.Format("Could not find enum value with OData Query operator '{0}'.", oDataQueryOperatorString));
			}

			return result;
		}
	}
}
