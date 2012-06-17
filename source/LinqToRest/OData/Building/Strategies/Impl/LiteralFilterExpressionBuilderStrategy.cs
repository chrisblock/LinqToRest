using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

using LinqToRest.OData.Filters;

namespace LinqToRest.OData.Building.Strategies.Impl
{
	public class LiteralFilterExpressionBuilderStrategy : IFilterExpressionBuilderStrategy
	{
		// TODO: split this up into strategies...somehow??
		// TODO: come up with some way to share these regular expressions with the FilterQueryPartParserStrategy
		// TODO: reconcile this with the odata URI ABNF: http://www.odata.org/media/30002/OData%20ABNF.html
		private static readonly IEnumerable<Tuple<Regex, Func<Match, FilterExpression>>> RegexToParserMappings = new List<Tuple<Regex, Func<Match, FilterExpression>>>
		{
			new Tuple<Regex, Func<Match, FilterExpression>>(new Regex(@"^null$", RegexOptions.IgnoreCase), ParseNull),
			new Tuple<Regex, Func<Match, FilterExpression>>(new Regex(@"^true|false$", RegexOptions.IgnoreCase), ParseBoolean),
			new Tuple<Regex, Func<Match, FilterExpression>>(new Regex(@"^'([^'\\]*(?:\\.[^'\\]*)*)'$"), ParseString),
			new Tuple<Regex, Func<Match, FilterExpression>>(new Regex(@"^guid'([0-9A-F]{8}\-[0-9A-F]{4}\-[0-9A-F]{4}\-[0-9A-F]{4}\-[0-9A-F]{12})'$", RegexOptions.IgnoreCase), ParseGuid),
			new Tuple<Regex, Func<Match, FilterExpression>>(new Regex(@"^datetime'([^']+)'$", RegexOptions.IgnoreCase), ParseDateTime),
			new Tuple<Regex, Func<Match, FilterExpression>>(new Regex(@"^datetimeoffset'([^']+)'$", RegexOptions.IgnoreCase), ParseDateTimeOffset),
			new Tuple<Regex, Func<Match, FilterExpression>>(new Regex(@"^time'([^']+)'$", RegexOptions.IgnoreCase), ParseTime),
			new Tuple<Regex, Func<Match, FilterExpression>>(new Regex(@"^((?:\d*\.)?\d+)m$", RegexOptions.IgnoreCase), ParseDecimal),
			new Tuple<Regex, Func<Match, FilterExpression>>(new Regex(@"^(\d+)$", RegexOptions.IgnoreCase), ParseInteger),
			new Tuple<Regex, Func<Match, FilterExpression>>(new Regex(@"^((?:\d*\.)?\d+)$", RegexOptions.IgnoreCase), ParseDouble),
			// TODO: this will incorrectly match string enum values; consider lexing and then parsing (with grammar rules)
			new Tuple<Regex, Func<Match, FilterExpression>>(new Regex(@"^\w+$", RegexOptions.IgnoreCase), ParseMemberAccess),
			new Tuple<Regex, Func<Match, FilterExpression>>(new Regex(@"^((?:\w+\.)+\w+)$", RegexOptions.IgnoreCase), ParseTypeLiteral)
		};

		public FilterExpression BuildExpression(Stack<string> stack)
		{
			if (stack == null)
			{
				throw new ArgumentNullException("stack", "Cannot build literal expression with a null expression stack.");
			}

			if (stack.Count < 1)
			{
				throw new ArgumentException("Cannot build literal expression with an empty expression stack.");
			}

			return Parse(stack.Pop());
		}

		private static FilterExpression Parse(string literal)
		{
			FilterExpression result = null;

			foreach (var mapping in RegexToParserMappings)
			{
				var regex = mapping.Item1;
				var func = mapping.Item2;

				var match = regex.Match(literal);

				if (match.Success == true)
				{
					result = func(match);

					break;
				}
			}

			return result;
		}

		private static FilterExpression ParseNull(Match arg)
		{
			return FilterExpression.Constant(null, typeof (object));
		}

		private static FilterExpression ParseBoolean(Match m)
		{
			var boolString = m.Value;

			bool result;
			if (Boolean.TryParse(boolString, out result) == false)
			{
				throw new ArgumentException(String.Format("'{0}' could not be parsed as a boolean value.", boolString));
			}

			return FilterExpression.Constant(result, typeof(bool));
		}

		private static FilterExpression ParseString(Match m)
		{
			var str = m.Groups.Cast<Group>().Skip(1).First().Value;

			// TODO: is there a better way to do this?? have to un-escape single quotes...
			str = str.Replace("\\'", "'");

			return FilterExpression.Constant(str, typeof(string));
		}

		private static FilterExpression ParseGuid(Match m)
		{
			var guidString = m.Groups.Cast<Group>().Skip(1).First().Value;
			var guid = Guid.Parse(guidString);

			return FilterExpression.Constant(guid, typeof(Guid));
		}

		private static FilterExpression ParseDateTime(Match m)
		{
			var dateTimeString = m.Groups.Cast<Group>().Skip(1).First().Value;

			// TODO: is there a better way to parse UTC times?
			var style = dateTimeString.EndsWith("Z")
				? DateTimeStyles.AdjustToUniversal
				: DateTimeStyles.None;

			var datetime = DateTime.ParseExact(dateTimeString, "yyyy'-'MM'-'dd'T'HH':'mm':'ssK", CultureInfo.InvariantCulture, style);

			return FilterExpression.Constant(datetime, typeof(DateTime));
		}

		private static FilterExpression ParseDateTimeOffset(Match m)
		{
			var dateTimeOffsetString = m.Groups.Cast<Group>().Skip(1).First().Value;
			var time = DateTimeOffset.Parse(dateTimeOffsetString);

			return FilterExpression.Constant(time, typeof(DateTimeOffset));
		}

		private static FilterExpression ParseTime(Match m)
		{
			var time = TimeSpan.Parse(m.Groups.Cast<Group>().Skip(1).First().Value);

			return FilterExpression.Constant(time, typeof(TimeSpan));
		}

		private static FilterExpression ParseDecimal(Match m)
		{
			var dec = Decimal.Parse(m.Groups.Cast<Group>().Skip(1).First().Value);

			return FilterExpression.Constant(dec, typeof (decimal));
		}

		private static FilterExpression ParseDouble(Match m)
		{
			var dec = Double.Parse(m.Groups.Cast<Group>().Skip(1).First().Value);

			return FilterExpression.Constant(dec, typeof(double));
		}

		private static FilterExpression ParseInteger(Match m)
		{
			var dec = Int32.Parse(m.Groups.Cast<Group>().Skip(1).First().Value);

			return FilterExpression.Constant(dec, typeof(int));
		}

		private static FilterExpression ParseMemberAccess(Match m)
		{
			var memberName = m.Value;

			return FilterExpression.MemberAccess(memberName);
		}

		private static FilterExpression ParseTypeLiteral(Match m)
		{
			var typeName = m.Value;

			var type = Type.GetType(typeName);

			if (type == null)
			{
				throw new NotSupportedException(String.Format("Cannot constrain to instances of type '{0}'. It is unrecognized.", typeName));
			}

			return FilterExpression.Constant(type, typeof(Type));
		}
	}
}
