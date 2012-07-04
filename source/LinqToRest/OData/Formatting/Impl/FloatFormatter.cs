using System;

namespace LinqToRest.OData.Formatting.Impl
{
	public class FloatFormatter : AbstractTypeFormatter<float>
	{
		protected override string Format(float value)
		{
			return String.Format("{0}f", value);
		}
	}
}
