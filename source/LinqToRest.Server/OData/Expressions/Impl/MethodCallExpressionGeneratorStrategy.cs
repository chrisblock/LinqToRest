using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using LinqToRest.OData.Filters;

namespace LinqToRest.Server.OData.Expressions.Impl
{
	public class MethodCallExpressionGeneratorStrategy : IMethodCallExpressionGeneratorStrategy
	{
		private readonly IDictionary<Function, IMethodCallExpressionGeneratorStrategy> _strategies;

		public MethodCallExpressionGeneratorStrategy()
		{
			_strategies = new Dictionary<Function, IMethodCallExpressionGeneratorStrategy>
			{
				{Function.Floor, new MathMethodCallExpressionGeneratorStrategy()},
				{Function.Ceiling, new MathMethodCallExpressionGeneratorStrategy()},
				{Function.Round, new MathMethodCallExpressionGeneratorStrategy()},

				{Function.EndsWith, new InstanceMethodCallExpressionGeneratorStrategy()},
				{Function.IndexOf, new InstanceMethodCallExpressionGeneratorStrategy()},
				{Function.Replace, new InstanceMethodCallExpressionGeneratorStrategy()},
				{Function.StartsWith, new InstanceMethodCallExpressionGeneratorStrategy()},
				{Function.Substring, new InstanceMethodCallExpressionGeneratorStrategy()},
				{Function.SubstringOf, new InstanceMethodCallExpressionGeneratorStrategy()},
				{Function.ToLower, new InstanceMethodCallExpressionGeneratorStrategy()},
				{Function.ToUpper, new InstanceMethodCallExpressionGeneratorStrategy()},
				{Function.Trim, new InstanceMethodCallExpressionGeneratorStrategy()},

				{Function.Concat, new ConcatMethodCallExpressionGeneratorStrategy()},

				{Function.Cast, new CastMethodCallExpressionGeneratorStrategy()},
				{Function.IsOf, new IsOfMethodCallExpressionGeneratorStrategy()},

				{Function.Day, new PropertyExpressionGeneratorStrategy()},
				{Function.Days, new PropertyExpressionGeneratorStrategy()},
				{Function.Hour, new PropertyExpressionGeneratorStrategy()},
				{Function.Hours, new PropertyExpressionGeneratorStrategy()},
				{Function.Length, new PropertyExpressionGeneratorStrategy()},
				{Function.Minute, new PropertyExpressionGeneratorStrategy()},
				{Function.Minutes, new PropertyExpressionGeneratorStrategy()},
				{Function.Month, new PropertyExpressionGeneratorStrategy()},
				{Function.Second, new PropertyExpressionGeneratorStrategy()},
				{Function.Seconds, new PropertyExpressionGeneratorStrategy()},
				{Function.Year, new PropertyExpressionGeneratorStrategy()},
				{Function.Years, new PropertyExpressionGeneratorStrategy()}
			};
		}

		public Expression Generate(Function method, IEnumerable<Expression> arguments)
		{
			Expression result;

			IMethodCallExpressionGeneratorStrategy strategy;
			if (_strategies.TryGetValue(method, out strategy))
			{
				result = strategy.Generate(method, arguments);
			}
			else
			{
				throw new ArgumentException("Method not found.");
			}

			return result;
		}
	}
}
