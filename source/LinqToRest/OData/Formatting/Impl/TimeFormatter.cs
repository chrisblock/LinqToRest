using System;

namespace LinqToRest.OData.Formatting.Impl
{
	public class TimeFormatter : AbstractTypeFormatter<TimeSpan>
	{
		protected override string Format(TimeSpan value)
		{
			// TODO: calculate Years and Months manually, despite their variable length nature..??
			return String.Format("time'{0:'P0Y0M'd'DT'h'H'm'M's'.'ffffff'S'}'", value);
		}
	}
}
