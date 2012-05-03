using System.Linq.Expressions;

namespace LinqToRest.OData.Filters
{
	public enum ODataQueryFilterExpressionOperator
	{
		[DotNetOperator(ExpressionType.Add)]
		[ODataQueryOperator("add")]
		Add,

		[DotNetOperator(ExpressionType.AndAlso)]
		[ODataQueryOperator("and")]
		And,

		[DotNetOperator(ExpressionType.Divide)]
		[ODataQueryOperator("div")]
		Divide,

		[DotNetOperator(ExpressionType.Equal)]
		[ODataQueryOperator("eq")]
		Equal,

		[DotNetOperator(ExpressionType.GreaterThan)]
		[ODataQueryOperator("gt")]
		GreaterThan,

		[DotNetOperator(ExpressionType.GreaterThanOrEqual)]
		[ODataQueryOperator("ge")]
		GreaterThanOrEqual,

		[DotNetOperator(ExpressionType.LessThan)]
		[ODataQueryOperator("lt")]
		LessThan,

		[DotNetOperator(ExpressionType.LessThanOrEqual)]
		[ODataQueryOperator("le")]
		LessThanOrEqual,

		[DotNetOperator(ExpressionType.Modulo)]
		[ODataQueryOperator("mod")]
		Modulo,

		[DotNetOperator(ExpressionType.Multiply)]
		[ODataQueryOperator("mul")]
		Multiply,

		[DotNetOperator(ExpressionType.NotEqual)]
		[ODataQueryOperator("ne")]
		NotEqual,

		[DotNetOperator(ExpressionType.OrElse)]
		[ODataQueryOperator("or")]
		Or,

		[DotNetOperator(ExpressionType.Subtract)]
		[ODataQueryOperator("sub")]
		Subtract,

		[DotNetOperator(ExpressionType.Not)]
		[ODataQueryOperator("not")]
		Not,

		[DotNetOperator(ExpressionType.Negate)]
		[ODataQueryOperator("-")]
		Negate
	}
}
