using System.Linq.Expressions;

namespace LinqToRest.OData.Filters
{
	public enum FilterExpressionOperator
	{
		Unknown,

		[DotNetOperator(ExpressionType.Add)]
		[FilterOperator("add")]
		Add,

		[DotNetOperator(ExpressionType.AndAlso)]
		[FilterOperator("and")]
		And,

		[DotNetOperator(ExpressionType.Divide)]
		[FilterOperator("div")]
		Divide,

		[DotNetOperator(ExpressionType.Equal)]
		[FilterOperator("eq")]
		Equal,

		[DotNetOperator(ExpressionType.GreaterThan)]
		[FilterOperator("gt")]
		GreaterThan,

		[DotNetOperator(ExpressionType.GreaterThanOrEqual)]
		[FilterOperator("ge")]
		GreaterThanOrEqual,

		[DotNetOperator(ExpressionType.LessThan)]
		[FilterOperator("lt")]
		LessThan,

		[DotNetOperator(ExpressionType.LessThanOrEqual)]
		[FilterOperator("le")]
		LessThanOrEqual,

		[DotNetOperator(ExpressionType.Modulo)]
		[FilterOperator("mod")]
		Modulo,

		[DotNetOperator(ExpressionType.Multiply)]
		[FilterOperator("mul")]
		Multiply,

		[DotNetOperator(ExpressionType.NotEqual)]
		[FilterOperator("ne")]
		NotEqual,

		[DotNetOperator(ExpressionType.OrElse)]
		[FilterOperator("or")]
		Or,

		[DotNetOperator(ExpressionType.Subtract)]
		[FilterOperator("sub")]
		Subtract,

		[DotNetOperator(ExpressionType.Not)]
		[FilterOperator("not")]
		Not,

		[DotNetOperator(ExpressionType.Negate)]
		[FilterOperator("-")]
		Negate
	}
}
