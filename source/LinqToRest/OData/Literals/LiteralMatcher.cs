using LinqToRest.OData.Literals.Impl;

namespace LinqToRest.OData.Literals
{
	public static class LiteralMatcher
	{
		//// TODO: this will incorrectly match string enum values; consider lexing and then parsing (with grammar rules)
		//new Tuple<Regex, Func<Match, FilterExpression>>(new Regex(@"^\w+$", RegexOptions.IgnoreCase), ParseMemberAccess),
		//new Tuple<Regex, Func<Match, FilterExpression>>(new Regex(@"^((?:\w+\.)+\w+)$", RegexOptions.IgnoreCase), ParseTypeLiteral)

		public static ILiteral Null = new NullLiteral();
		public static ILiteral Boolean = new BooleanLiteral();
		public static ILiteral String = new StringLiteral();
		public static ILiteral Guid = new GuidLiteral();
		public static ILiteral DateTime = new DateTimeLiteral();
		public static ILiteral DateTimeOffset = new DateTimeOffsetLiteral();
		public static ILiteral Time = new TimeLiteral();
		public static ILiteral Byte = new ByteLiteral();
		public static ILiteral Short = new ShortLiteral();
		public static ILiteral Integer = new IntegerLiteral();
		public static ILiteral Long = new LongLiteral();
		public static ILiteral Float = new FloatLiteral();
		public static ILiteral Double = new DoubleLiteral();
		public static ILiteral Decimal = new DecimalLiteral();
		public static ILiteral PrimitiveType = new PrimitiveLiteral();
	}
}
