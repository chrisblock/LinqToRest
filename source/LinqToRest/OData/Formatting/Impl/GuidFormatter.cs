using System;

namespace LinqToRest.OData.Formatting.Impl
{
	public class GuidFormatter : AbstractTypeFormatter<Guid>
	{
		protected override string Format(Guid value)
		{
			return String.Format("guid'{0}'", value);
		}
	}
}
