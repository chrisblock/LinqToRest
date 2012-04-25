namespace LinqToRest.OData
{
	public static class QueryParameters
	{
		public static string Expand { get { return "$expand"; } }
		public static string Filter { get { return "$filter"; } }
		public static string Format { get { return "$format"; } }
		public static string OrderBy { get { return "$orderby"; } }
		public static string Select { get { return "$select"; } }
		public static string Skip { get { return "$skip"; } }
		public static string SkipToken { get { return "$skiptoken"; } }
		public static string Top { get { return "$top"; } }
	}
}
