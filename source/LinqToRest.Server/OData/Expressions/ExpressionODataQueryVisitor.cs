using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

using LinqToRest.OData;
using LinqToRest.OData.Filters;

namespace LinqToRest.Server.OData.Expressions
{
	public class ExpressionODataQueryVisitor : ODataQueryVisitor, IODataQueryTranslator
	{
		private readonly IFilterExpressionBuilder _filterExpressionBuilder;

		private bool _isOrdered = false;

		private Type _itemType;

		private Expression _expression;

		public ExpressionODataQueryVisitor(IFilterExpressionBuilder filterExpressionBuilder)
		{
			_filterExpressionBuilder = filterExpressionBuilder;
		}

		public LambdaExpression Translate<T>(ODataQuery query)
		{
			var type = typeof (T);

			return Translate(type, query);
		}

		public LambdaExpression Translate(Type type, ODataQuery query)
		{
			_itemType = type;

			var parameterType = typeof (IQueryable<>).MakeGenericType(type);

			var parameter = ExpressionHelper.CreateParameter(parameterType);

			_expression = parameter;

			Visit(query);

			return Expression.Lambda(_expression, false, parameter);
		}

		protected override CountQueryPart VisitCount(CountQueryPart count)
		{
			_expression = Expression.Call(typeof (Queryable), "Count", new[] {_itemType}, _expression);

			return base.VisitCount(count);
		}

		protected override FilterQueryPart VisitFilter(FilterQueryPart filter)
		{
			var whereClause = _filterExpressionBuilder.BuildExpression(_itemType, filter.FilterExpression);

			_expression = Expression.Call(typeof (Queryable), "Where", new[] {_itemType}, _expression, whereClause);

			return base.VisitFilter(filter);
		}

		protected override ODataOrdering VisitOrdering(ODataOrdering ordering)
		{
			var method = "ThenBy";

			if (_isOrdered == false)
			{
				_isOrdered = true;

				method = "OrderBy";
			}

			var methodName = (ordering.Direction == ODataOrderingDirection.Asc)
				? method
				: String.Format("{0}{1}", method, "Descending");

			var lambda = ExpressionHelper.Lambda(_itemType, ordering.Field);

			var propertyType = lambda.ReturnType;

			_expression = Expression.Call(typeof (Queryable), methodName, new[] {_itemType, propertyType}, new[] {_expression, lambda});

			return base.VisitOrdering(ordering);
		}

		protected override SkipQueryPart VisitSkip(SkipQueryPart skip)
		{
			if (skip.NumberToSkip.HasValue)
			{
				var skipNumber = Expression.Constant(skip.NumberToSkip.Value, typeof (int));

				_expression = Expression.Call(typeof (Queryable), "Skip", new[] {_itemType}, new[] {_expression, skipNumber});
			}

			return base.VisitSkip(skip);
		}

		protected override TopQueryPart VisitTop(TopQueryPart top)
		{
			if (top.NumberToTake.HasValue)
			{
				var takeNumber = Expression.Constant(top.NumberToTake.Value, typeof (int));

				_expression = Expression.Call(typeof (Queryable), "Take", new[] {_itemType}, new[] {_expression, takeNumber});
			}

			return base.VisitTop(top);
		}

		protected override SelectQueryPart VisitSelect(SelectQueryPart select)
		{
			var parameter = Expression.Parameter(_itemType, "select");

			var memberExpressions = select.Selectors
				.Select(selector => GetMemberAccessExpression(parameter, selector))
				.ToList();

			if (memberExpressions.Count > 1)
			{
				var properties = memberExpressions
					.Select(x => x.Member).Cast<PropertyInfo>()
					.Select(x => new Property
					{
						Type = x.PropertyType,
						Name = x.Name
					});

				var selectType = AnonymousTypeManager.BuildType(properties);

				var memberBindings = new List<MemberBinding>();

				foreach (var memberExpression in memberExpressions)
				{
					var property = selectType.GetField(memberExpression.Member.Name);

					var memberBinding = Expression.Bind(property, memberExpression);

					memberBindings.Add(memberBinding);
				}

				var constructor = Expression.New(selectType);

				var initialization = Expression.MemberInit(constructor, memberBindings);

				var lambda = Expression.Lambda(initialization, false, parameter);

				_expression = Expression.Call(typeof (Queryable), "Select", new[] { _itemType, selectType }, _expression, lambda);
			}
			else if (memberExpressions.Count > 0)
			{
				var memberExpression = memberExpressions.Single();

				var selectType = memberExpression.Type;

				var lambda = Expression.Lambda(memberExpression, false, parameter);

				_expression = Expression.Call(typeof (Queryable), "Select", new[] { _itemType, selectType }, _expression, lambda);
			}

			return base.VisitSelect(select);
		}

		private static MemberExpression GetMemberAccessExpression(ParameterExpression parameter, MemberAccessFilterExpression selector)
		{
			MemberExpression result;

			if (selector.Instance == null)
			{
				var property = parameter.Type.GetProperty(selector.Member);

				result = Expression.MakeMemberAccess(parameter, property);
			}
			else
			{
				if (selector.Instance.ExpressionType == FilterExpressionType.MemberAccess)
				{
					var parent = GetMemberAccessExpression(parameter, (MemberAccessFilterExpression) selector.Instance);

					var property = parent.Type.GetProperty(selector.Member);

					if (property == null)
					{
						throw new ArgumentException(String.Format("Could not find property '{0}' on type '{1}'.", selector.Member, parent.Type));
					}

					result = Expression.MakeMemberAccess(parent, property);
				}
				else
				{
					throw new NotSupportedException("Accessing members of non-member expressions is not supported.");
				}
			}

			return result;
		}
	}
}
