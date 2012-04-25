namespace LinqToRest.OData
{
	public static class FilterFunctions
	{
		public static string ToUpper { get { return "toupper"; } }
		public static string ToLower { get { return "tolower"; } }
		public static string Trim { get { return "trim"; } }
		public static string Length { get { return "length"; } }
		public static string Year { get { return "year"; } }
		public static string Month { get { return "month"; } }
		public static string Day { get { return "day"; } }
		public static string Hour { get { return "hour"; } }
		public static string Minute { get { return "minute"; } }
		public static string Second { get { return "second"; } }
		public static string Ceiling { get { return "ceiling"; } }
		public static string Floor { get { return "floor"; } }
		public static string Round { get { return "round"; } }

		public static string IsOf { get { return "isof"; } }
		public static string Cast { get { return "cast"; } }
		public static string IndexOf { get { return "indexof"; } }
		public static string StartsWith { get { return "startswith"; } }
		public static string EndsWith { get { return "endswith"; } }
		public static string Substring { get { return "substring"; } }
		public static string SubstringOf { get { return "substringof"; } }

		public static string Replace { get { return "replace"; } }
	}
}
