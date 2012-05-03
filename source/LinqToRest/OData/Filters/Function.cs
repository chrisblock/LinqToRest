namespace LinqToRest.OData.Filters
{
	public enum Function
	{
		[DotNetMethod("ToUpper")]
		[ODataQueryMethod("toupper")]
		ToUpper,

		[DotNetMethod("ToLower")]
		[ODataQueryMethod("tolower")]
		ToLower,

		[DotNetMethod("Trim")]
		[ODataQueryMethod("trim")]
		Trim,

		[DotNetMethod("Length")]
		[ODataQueryMethod("length")]
		Length,

		[DotNetMethod("Concat")]
		[ODataQueryMethod("concat")]
		Concat,

		[DotNetMethod("IndexOf")]
		[ODataQueryMethod("indexof")]
		IndexOf,

		[DotNetMethod("StartsWith")]
		[ODataQueryMethod("startswith")]
		StartsWith,

		[DotNetMethod("EndsWith")]
		[ODataQueryMethod("endswith")]
		EndsWith,

		[DotNetMethod("Substring")]
		[ODataQueryMethod("substring")]
		Substring,

		[DotNetMethod("Contains")]
		[ODataQueryMethod("substringof")]
		SubstringOf,

		[DotNetMethod("Replace")]
		[ODataQueryMethod("replace")]
		Replace,

		// datetime and datetimeoffset functions
		[DotNetMethod("Year")]
		[ODataQueryMethod("year")]
		Year,

		[DotNetMethod("Month")]
		[ODataQueryMethod("month")]
		Month,

		[DotNetMethod("Day")]
		[ODataQueryMethod("day")]
		Day,

		[DotNetMethod("Hour")]
		[ODataQueryMethod("hour")]
		Hour,

		[DotNetMethod("Minute")]
		[ODataQueryMethod("minute")]
		Minute,

		[DotNetMethod("Second")]
		[ODataQueryMethod("second")]
		Second,

		// time functions
		[DotNetMethod("TotalYears")]
		[ODataQueryMethod("years")]
		Years,

		[DotNetMethod("TotalDays")]
		[ODataQueryMethod("days")]
		Days,

		[DotNetMethod("TotalHours")]
		[ODataQueryMethod("hours")]
		Hours,

		[DotNetMethod("TotalMinutes")]
		[ODataQueryMethod("minutes")]
		Minutes,

		[DotNetMethod("TotalSeconds")]
		[ODataQueryMethod("seconds")]
		Seconds,

		// decimal and double functions
		[DotNetMethod("Ceiling")]
		[ODataQueryMethod("ceiling")]
		Ceiling,

		[DotNetMethod("Floor")]
		[ODataQueryMethod("floor")]
		Floor,

		[DotNetMethod("Round")]
		[ODataQueryMethod("round")]
		Round,

		// type functions
		[ODataQueryMethod("isof")]
		IsOf,

		[ODataQueryMethod("cast")]
		Cast,
	}
}
