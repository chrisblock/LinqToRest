using System;

namespace LinqToRest.OData.Formatting.Impl
{
	public class StringFormatter : AbstractTypeFormatter<string>
	{
		protected override string Format(string value)
		{
			return String.Format("'{0}'", value.Replace("'", "\\'"));
		}
	}
}
