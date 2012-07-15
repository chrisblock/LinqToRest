namespace LinqToRest.OData
{
	public enum ODataQueryPartType
	{
		Unknown,

		[UrlParameter("$count")]
		Count,

		[UrlParameter("$expand")]
		Expand,

		[UrlParameter("$filter")]
		Filter,

		[UrlParameter("$format")]
		Format,

		[UrlParameter("$inlinecount")]
		InlineCount,

		[UrlParameter("$orderby")]
		OrderBy,
		Ordering,

		[UrlParameter("$select")]
		Select,

		[UrlParameter("$skip")]
		Skip,

		[UrlParameter("$skiptoken")]
		SkipToken,

		[UrlParameter("$top")]
		Top
	}
}
