using System;

namespace LinqToRest.OData
{
	public class ODataSkipQueryPart : ODataQuery
	{
		public override ODataQueryPartType QueryType { get { return ODataQueryPartType.Skip; } }

		public int? NumberToSkip { get; private set; }

		public ODataSkipQueryPart(int? numberToSkip)
		{
			NumberToSkip = numberToSkip;
		}

		public override string ToString()
		{
			var result = String.Empty;

			if (NumberToSkip.HasValue)
			{
				result = BuildParameterString(NumberToSkip.Value);
			}

			return result;
		}
	}
}
