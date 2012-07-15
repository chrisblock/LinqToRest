using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace LinqToRest.OData.Parsing.Impl
{
	public class TimeFilterExpressionParserStrategy : AbstractFilterExpressionParserStrategy<TimeSpan>
	{
		protected override TimeSpan Parse(string text)
		{
			// TODO: make this resilient enough so that the different parts are optional
			var match = Regex.Match(text, @"time'P(\d+)Y(\d+)M(\d+)DT(\d+)H(\d+)M(\d+)\.(\d+)S'");

			if (match.Success == false)
			{
				throw new ArgumentException(String.Format("'{0}' could not be parsed as a {1}.", text, Type));
			}

			var groups = match.Groups.Cast<Group>().Skip(1).ToArray();

			var years = Int32.Parse(groups[0].Value);
			var months = Int32.Parse(groups[1].Value);
			var days = Int32.Parse(groups[2].Value);
			var hours = Int32.Parse(groups[3].Value);
			var minutes = Int32.Parse(groups[4].Value);
			var seconds = Int32.Parse(groups[5].Value);

			var magnitude = groups[6].Value.Length;

			var fractionalSeconds = Int32.Parse(groups[6].Value);

			var milliseconds = (int)(fractionalSeconds / Math.Pow(10, magnitude - 3));

			var totalDays = (years * 365) + (months * 30) + days;

			var result = new TimeSpan(totalDays, hours, minutes, seconds, milliseconds);

			return result;
		}
	}
}
