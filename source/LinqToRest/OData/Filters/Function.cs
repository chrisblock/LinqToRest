namespace LinqToRest.OData.Filters
{
	public enum Function
	{
		Unknown,

		[Arity(1)]
		[DotNetMethod("ToUpper")]
		[FilterMethod("toupper")]
		ToUpper,

		[Arity(1)]
		[DotNetMethod("ToLower")]
		[FilterMethod("tolower")]
		ToLower,

		[Arity(1)]
		[DotNetMethod("Trim")]
		[FilterMethod("trim")]
		Trim,

		[Arity(1)]
		[DotNetMethod("Length")]
		[FilterMethod("length")]
		Length,

		[Arity(2)]
		[DotNetMethod("Concat")]
		[FilterMethod("concat")]
		Concat,

		[Arity(2)]
		[DotNetMethod("IndexOf")]
		[FilterMethod("indexof")]
		IndexOf,

		[Arity(2)]
		[DotNetMethod("StartsWith")]
		[FilterMethod("startswith")]
		StartsWith,

		[Arity(2)]
		[DotNetMethod("EndsWith")]
		[FilterMethod("endswith")]
		EndsWith,

		[Arity(2)]
		[DotNetMethod("Substring")]
		[FilterMethod("substring")]
		Substring,

		[Arity(2)]
		[DotNetMethod("Contains")]
		[FilterMethod("substringof")]
		SubstringOf,

		[Arity(3)]
		[DotNetMethod("Replace")]
		[FilterMethod("replace")]
		Replace,

		// datetime and datetimeoffset functions
		[Arity(1)]
		[DotNetMethod("Year")]
		[FilterMethod("year")]
		Year,

		[Arity(1)]
		[DotNetMethod("Month")]
		[FilterMethod("month")]
		Month,

		[Arity(1)]
		[DotNetMethod("Day")]
		[FilterMethod("day")]
		Day,

		[Arity(1)]
		[DotNetMethod("Hour")]
		[FilterMethod("hour")]
		Hour,

		[Arity(1)]
		[DotNetMethod("Minute")]
		[FilterMethod("minute")]
		Minute,

		[Arity(1)]
		[DotNetMethod("Second")]
		[FilterMethod("second")]
		Second,

		// time functions
		[Arity(1)]
		[DotNetMethod("TotalYears")]
		[FilterMethod("years")]
		Years,

		[Arity(1)]
		[DotNetMethod("TotalDays")]
		[FilterMethod("days")]
		Days,

		[Arity(1)]
		[DotNetMethod("TotalHours")]
		[FilterMethod("hours")]
		Hours,

		[Arity(1)]
		[DotNetMethod("TotalMinutes")]
		[FilterMethod("minutes")]
		Minutes,

		[Arity(1)]
		[DotNetMethod("TotalSeconds")]
		[FilterMethod("seconds")]
		Seconds,

		// decimal and double functions
		[Arity(1)]
		[DotNetMethod("Ceiling")]
		[FilterMethod("ceiling")]
		Ceiling,

		[Arity(1)]
		[DotNetMethod("Floor")]
		[FilterMethod("floor")]
		Floor,

		[Arity(1)]
		[DotNetMethod("Round")]
		[FilterMethod("round")]
		Round,

		// type functions
		[Arity(2)]
		[FilterMethod("isof")]
		IsOf,

		[Arity(2)]
		[FilterMethod("cast")]
		Cast,
	}
}
