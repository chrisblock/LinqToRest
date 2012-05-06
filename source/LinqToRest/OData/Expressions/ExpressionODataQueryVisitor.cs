using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace LinqToRest.OData.Expressions
{
	public class ExpressionODataQueryVisitor : ODataQueryVisitor, IODataQueryTranslator
	{
		private readonly IODataQueryFilterExpressionBuilder _filterExpressionBuilder;

		private bool _isOrdered = false;

		private Type _itemType;

		private Expression _expression;

		public ExpressionODataQueryVisitor() : this(DependencyResolver.Current.GetInstance<IODataQueryFilterExpressionBuilder>())
		{
		}

		public ExpressionODataQueryVisitor(IODataQueryFilterExpressionBuilder filterExpressionBuilder)
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
			string methodName;

			if (_isOrdered == false)
			{
				_isOrdered = true;

				methodName = (ordering.Direction == ODataOrderingDirection.Asc)
					? "OrderBy"
					: "OrderByDescending";
			}
			else
			{
				methodName = (ordering.Direction == ODataOrderingDirection.Asc)
					? "ThenBy"
					: "ThenByDescending";
			}

			var lambda = ExpressionHelper.Lambda(_itemType, ordering.Field);

			_expression = Expression.Call(typeof (Queryable), methodName, new[] {_itemType}, new[] {_expression, lambda});

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
			var properties = new List<PropertyInfo>();

			foreach (var selector in select.Selectors)
			{
				if (selector.Instance == null)
				{
					var property = _itemType.GetProperty(selector.Member);

					properties.Add(property);
				}
				else
				{
					throw new ArgumentException(String.Format("Property '{0}' is not on type '{1}'.", selector.Member, _itemType));
				}
			}

			var selectType = AnonymousTypeManager.BuildType(properties);

			var parameter = Expression.Parameter(_itemType);

			var memberBindings = new List<MemberBinding>();

			foreach (var propertyInfo in properties)
			{
				var property = selectType.GetProperty(propertyInfo.Name);

				var memberAccess = Expression.MakeMemberAccess(parameter, property);

				var memberBinding = Expression.Bind(property, memberAccess);

				memberBindings.Add(memberBinding);
			}

			var constructor = Expression.New(selectType);

			var initialization = Expression.MemberInit(constructor, memberBindings);

			var lambda = Expression.Lambda(initialization, false, parameter);

			_expression = Expression.Call(typeof (Queryable), "Select", new[] {_itemType, selectType}, _expression, lambda);

			return base.VisitSelect(select);
		}
	}
}
