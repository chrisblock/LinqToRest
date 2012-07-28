using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LinqToRest.OData.Filters
{
	public static class FilterExpressionOperatorEnumExtensions
	{
		private static readonly IDictionary<FilterExpressionOperator, ExpressionType> FilterExpressionOperatorToDotNetExpressionType;
		private static readonly IDictionary<FilterExpressionOperator, string> FilterExpressionOperatorToODataQueryOperatorString;
		private static readonly IDictionary<ExpressionType, FilterExpressionOperator> DotNetExpressionTypeToFilterExpressionOperator;
		private static readonly IDictionary<string, FilterExpressionOperator> ODataQueryOperatorStringToFilterExpressionOperator;

		static FilterExpressionOperatorEnumExtensions()
		{
			var fields = Enum.GetNames(typeof (FilterExpressionOperator))
				.Select(typeof (FilterExpressionOperator).GetField)
				.ToList();

			FilterExpressionOperatorToDotNetExpressionType = fields
				.Where(x => x.GetCustomAttributes<DotNetOperatorAttribute>().Any())
				.ToDictionary(key => (FilterExpressionOperator)key.GetValue(null), value => value.GetCustomAttributes<DotNetOperatorAttribute>().Single().ExpressionType);

			FilterExpressionOperatorToODataQueryOperatorString = fields
				.Where(x => x.GetCustomAttributes<FilterOperatorAttribute>().Any())
				.ToDictionary(key => (FilterExpressionOperator)key.GetValue(null), value => value.GetCustomAttributes<FilterOperatorAttribute>().Single().Value);

			DotNetExpressionTypeToFilterExpressionOperator = FilterExpressionOperatorToDotNetExpressionType.ToDictionary(key => key.Value, value => value.Key);

			ODataQueryOperatorStringToFilterExpressionOperator = FilterExpressionOperatorToODataQueryOperatorString.ToDictionary(key => key.Value, value => value.Key);
		}

		public static bool IsUnaryOperator(this FilterExpressionOperator filterExpressionOperator)
		{
			return ((filterExpressionOperator == FilterExpressionOperator.Not) || (filterExpressionOperator == FilterExpressionOperator.Negate));
		}

		public static bool IsBinaryOperator(this FilterExpressionOperator filterExpressionOperator)
		{
			return ((filterExpressionOperator.IsUnaryOperator() == false) && (filterExpressionOperator != FilterExpressionOperator.Unknown));
		}

		public static ExpressionType GetDotNetExpressionType(this FilterExpressionOperator filterExpressionOperator)
		{
			ExpressionType expressionType;

			if (FilterExpressionOperatorToDotNetExpressionType.TryGetValue(filterExpressionOperator, out expressionType) == false)
			{
				throw new ArgumentException(String.Format("No .NET ExpressionType defined for '{0}'.", filterExpressionOperator));
			}

			return expressionType;
		}

		public static FilterExpressionOperator GetFromDotNetExpressionType(this ExpressionType expressionType)
		{
			FilterExpressionOperator result;

			if (DotNetExpressionTypeToFilterExpressionOperator.TryGetValue(expressionType, out result) == false)
			{
				throw new ArgumentException(String.Format("Could not find enum value with .NET ExpressionType '{0}'.", expressionType));
			}

			return result;
		}

		public static string GetODataQueryOperatorString(this FilterExpressionOperator oDataQueryOperator)
		{
			string result;

			if (FilterExpressionOperatorToODataQueryOperatorString.TryGetValue(oDataQueryOperator, out result) == false)
			{
				throw new ArgumentException(String.Format("No ODataQuery operator name defined for '{0}'.", oDataQueryOperator));
			}

			return result;
		}

		public static FilterExpressionOperator GetFromODataQueryOperatorString(this string oDataQueryOperatorString)
		{
			FilterExpressionOperator result;

			if (ODataQueryOperatorStringToFilterExpressionOperator.TryGetValue(oDataQueryOperatorString, out result) == false)
			{
				throw new ArgumentException(String.Format("Could not find enum value with OData Query operator '{0}'.", oDataQueryOperatorString));
			}

			return result;
		}
	}
}
