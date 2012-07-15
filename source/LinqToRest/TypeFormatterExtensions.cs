using LinqToRest.OData.Formatting;

namespace LinqToRest
{
	public static class TypeFormatterExtensions
	{
		public static string Format<T>(this ITypeFormatter formatter, T value)
		{
			return formatter.Format(typeof (T), value);
		}
	}
}
