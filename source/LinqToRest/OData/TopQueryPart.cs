using System;

namespace LinqToRest.OData
{
	public class TopQueryPart : ODataQueryPart
	{
		public override ODataQueryPartType QueryPartType { get { return ODataQueryPartType.Top; } }

		public int? NumberToTake { get; private set; }

		public TopQueryPart(int? numberToTake)
		{
			NumberToTake = numberToTake;
		}

		public override string ToString()
		{
			var result = String.Empty;

			if (NumberToTake.HasValue)
			{
				result = BuildParameterString(NumberToTake.Value);
			}

			return result;
		}
	}
}
