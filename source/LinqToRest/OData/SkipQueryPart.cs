using System;

namespace LinqToRest.OData
{
	public class SkipQueryPart : ODataQueryPart
	{
		public override ODataQueryPartType QueryPartType { get { return ODataQueryPartType.Skip; } }

		public int? NumberToSkip { get; private set; }

		public SkipQueryPart(int? numberToSkip)
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
