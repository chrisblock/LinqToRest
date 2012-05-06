namespace LinqToRest.OData
{
	public enum ODataQueryPartType
	{
		Complete,

		[UrlParameter("$count")]
		Count,

		[UrlParameter("$expand")]
		Expand,

		[UrlParameter("$filter")]
		Filter,
		FilterExpression,

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
