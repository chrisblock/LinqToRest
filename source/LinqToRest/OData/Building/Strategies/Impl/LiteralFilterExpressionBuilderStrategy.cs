using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

using LinqToRest.OData.Filters;

namespace LinqToRest.OData.Building.Strategies.Impl
{
	public class LiteralFilterExpressionBuilderStrategy : IODataFilterExpressionBuilderStrategy
	{
		// TODO: split this up into strategies...somehow??
		private static readonly IEnumerable<Tuple<Regex, Func<Match, ODataQueryFilterExpression>>> RegexToParserMappings = new List<Tuple<Regex, Func<Match, ODataQueryFilterExpression>>>
		{
			new Tuple<Regex, Func<Match, ODataQueryFilterExpression>>(new Regex(@"^true|false$", RegexOptions.IgnoreCase), ParseBoolean),
			new Tuple<Regex, Func<Match, ODataQueryFilterExpression>>(new Regex(@"^'([^']*(?:\\\\|[^\\]))'$"), ParseString),
			new Tuple<Regex, Func<Match, ODataQueryFilterExpression>>(new Regex(@"^guid'([0-9A-F]{8}\-[0-9A-F]{4}\-[0-9A-F]{4}\-[0-9A-F]{4}\-[0-9A-F]{12})'$", RegexOptions.IgnoreCase), ParseGuid),
			new Tuple<Regex, Func<Match, ODataQueryFilterExpression>>(new Regex(@"^datetime'([^']+)'$", RegexOptions.IgnoreCase), ParseDateTime),
			new Tuple<Regex, Func<Match, ODataQueryFilterExpression>>(new Regex(@"^datetimeoffset'([^']+)'$", RegexOptions.IgnoreCase), ParseDateTimeOffset),
			new Tuple<Regex, Func<Match, ODataQueryFilterExpression>>(new Regex(@"^time'([^']+)'$", RegexOptions.IgnoreCase), ParseTime),
			new Tuple<Regex, Func<Match, ODataQueryFilterExpression>>(new Regex(@"^((?:\d*\.)?\d+)m$", RegexOptions.IgnoreCase), ParseDecimal),
			new Tuple<Regex, Func<Match, ODataQueryFilterExpression>>(new Regex(@"^(\d+)$", RegexOptions.IgnoreCase), ParseInteger),
			new Tuple<Regex, Func<Match, ODataQueryFilterExpression>>(new Regex(@"^((?:\d*\.)?\d+)$", RegexOptions.IgnoreCase), ParseDouble),
			new Tuple<Regex, Func<Match, ODataQueryFilterExpression>>(new Regex(@"^\w+$", RegexOptions.IgnoreCase), ParseMemberAccess),
			new Tuple<Regex, Func<Match, ODataQueryFilterExpression>>(new Regex(@"^((?:\w+\.)+\w+)$", RegexOptions.IgnoreCase), ParseTypeLiteral)
		};

		public ODataQueryFilterExpression BuildExpression(Stack<string> stack)
		{
			return Parse(stack.Pop());
		}

		private static ODataQueryFilterExpression Parse(string literal)
		{
			ODataQueryFilterExpression result = null;

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

		private static ODataQueryFilterExpression ParseBoolean(Match m)
		{
			var boolString = m.Value;

			bool result;
			if (Boolean.TryParse(boolString, out result) == false)
			{
				throw new ArgumentException(String.Format("'{0}' could not be parsed as a boolean value.", boolString));
			}

			return ODataQueryFilterExpression.Constant(result, typeof(bool));
		}

		private static ODataQueryFilterExpression ParseString(Match m)
		{
			var str = m.Groups.Cast<Group>().Skip(1).First().Value;

			// TODO: is there a better way to do this?? have to un-escape single quotes...
			str = str.Replace("\\'", "'");

			return ODataQueryFilterExpression.Constant(str, typeof(string));
		}

		private static ODataQueryFilterExpression ParseGuid(Match m)
		{
			var guidString = m.Groups.Cast<Group>().Skip(1).First().Value;
			var guid = Guid.Parse(guidString);

			return ODataQueryFilterExpression.Constant(guid, typeof(Guid));
		}

		private static ODataQueryFilterExpression ParseDateTime(Match m)
		{
			var dateTimeString = m.Groups.Cast<Group>().Skip(1).First().Value;

			// TODO: is there a better way to parse UTC times?
			var style = dateTimeString.EndsWith("Z")
				? DateTimeStyles.AdjustToUniversal
				: DateTimeStyles.None;

			var datetime = DateTime.ParseExact(dateTimeString, "yyyy'-'MM'-'dd'T'HH':'mm':'ssK", CultureInfo.InvariantCulture, style);

			return ODataQueryFilterExpression.Constant(datetime, typeof(DateTime));
		}

		private static ODataQueryFilterExpression ParseDateTimeOffset(Match m)
		{
			var dateTimeOffsetString = m.Groups.Cast<Group>().Skip(1).First().Value;
			var time = DateTimeOffset.Parse(dateTimeOffsetString);

			return ODataQueryFilterExpression.Constant(time, typeof(DateTimeOffset));
		}

		private static ODataQueryFilterExpression ParseTime(Match m)
		{
			var time = TimeSpan.Parse(m.Groups.Cast<Group>().Skip(1).First().Value);

			return ODataQueryFilterExpression.Constant(time, typeof(TimeSpan));
		}

		private static ODataQueryFilterExpression ParseDecimal(Match m)
		{
			var dec = Decimal.Parse(m.Groups.Cast<Group>().Skip(1).First().Value);

			return ODataQueryFilterExpression.Constant(dec, typeof (decimal));
		}

		private static ODataQueryFilterExpression ParseDouble(Match m)
		{
			var dec = Double.Parse(m.Groups.Cast<Group>().Skip(1).First().Value);

			return ODataQueryFilterExpression.Constant(dec, typeof(double));
		}

		private static ODataQueryFilterExpression ParseInteger(Match m)
		{
			var dec = Int32.Parse(m.Groups.Cast<Group>().Skip(1).First().Value);

			return ODataQueryFilterExpression.Constant(dec, typeof(int));
		}

		private static ODataQueryFilterExpression ParseMemberAccess(Match m)
		{
			var memberName = m.Value;

			return ODataQueryFilterExpression.MemberAccess(memberName);
		}

		private static ODataQueryFilterExpression ParseTypeLiteral(Match m)
		{
			var typeName = m.Value;

			var type = Type.GetType(typeName);

			if (type == null)
			{
				throw new NotSupportedException(String.Format("Cannot constrain to instances of type '{0}'. It is unrecognized.", typeName));
			}

			return ODataQueryFilterExpression.Constant(type, typeof(Type));
		}
	}
}
