using System;
using System.Collections.Generic;

namespace LinqToRest.Server.OData.Lexing
{
	public abstract class AbstractNumericRegularExpressionTableLexerEntry : AbstractRegularExpressionTableLexerEntry
	{
		protected static string GenerateRegularExpression(long min, long max)
		{
			if (max <= min)
			{
				throw new ArgumentException("'max' cannot be less than or equal to 'min'.");
			}

			return String.Format(@"(?<!\S)(?:{0}|(?:{1}))(?![\w\.])", GeneratePossibilities(max), min);
		}

		protected static string GeneratePossibilities(long max)
		{
			var possibilities = new List<string>();

			var n = String.Format("{0}", max);

			possibilities.Add(String.Format(@"(?:\d{{1,{0}}})", n.Length - 1));

			for (var i = 0; i < n.Length; i++)
			{
				var prefix = String.Format("{0}", n.SubString(0, i));
				var variableDigits = (n.Length - i - 1 == 0)
					? String.Format("[0-{0}]", Int32.Parse(n.Substring(i, 1)))
					: String.Format("[0-{0}]", Int32.Parse(n.Substring(i, 1)) - 1);

				var possibility = (n.Length - i - 1 == 0)
					? String.Format(@"(?:{0}{1})", prefix, variableDigits)
					: String.Format(@"(?:{0}{1}\d{{{2}}})", prefix, variableDigits, n.Length - i - 1);

				possibilities.Add(possibility);
			}

			return String.Format("(?:[+-]?(?:{0}))", String.Join("|", possibilities));
		}

		protected static string GenerateUnsignedRegularExpression(ulong min, ulong max)
		{
			if (max <= min)
			{
				throw new ArgumentException("'max' cannot be less than or equal to 'min'.");
			}

			return String.Format(@"(?<!\S){0}(?![\w\.])", GenerateUnsignedPossibilities(max));
		}

		protected static string GenerateUnsignedPossibilities(ulong max)
		{
			var possibilities = new List<string>();

			var n = String.Format("{0}", max);

			possibilities.Add(String.Format(@"(?:\d{{1,{0}}})", n.Length - 1));

			for (var i = 0; i < n.Length; i++)
			{
				var prefix = String.Format("{0}", n.SubString(0, i));
				var variableDigits = (n.Length - i - 1 == 0)
					? String.Format("[0-{0}]", Int32.Parse(n.Substring(i, 1)))
					: String.Format("[0-{0}]", Int32.Parse(n.Substring(i, 1)) - 1);

				var possibility = (n.Length - i - 1 == 0)
					? String.Format(@"(?:{0}{1})", prefix, variableDigits)
					: String.Format(@"(?:{0}{1}\d{{{2}}})", prefix, variableDigits, n.Length - i - 1);

				possibilities.Add(possibility);
			}

			return String.Format(@"\+?(?:{0})", String.Join("|", possibilities));
		}

		protected AbstractNumericRegularExpressionTableLexerEntry(sbyte min, sbyte max) : base(GenerateRegularExpression(min, max))
		{
		}

		protected AbstractNumericRegularExpressionTableLexerEntry(short min, short max) : base(GenerateRegularExpression(min, max))
		{
		}

		protected AbstractNumericRegularExpressionTableLexerEntry(int min, int max) : base(GenerateRegularExpression(min, max))
		{
		}

		protected AbstractNumericRegularExpressionTableLexerEntry(long min, long max) : base(GenerateRegularExpression(min, max))
		{
		}

		protected AbstractNumericRegularExpressionTableLexerEntry(byte min, byte max) : base(GenerateUnsignedRegularExpression(min, max))
		{
		}

		protected AbstractNumericRegularExpressionTableLexerEntry(ushort min, ushort max) : base(GenerateUnsignedRegularExpression(min, max))
		{
		}

		protected AbstractNumericRegularExpressionTableLexerEntry(uint min, uint max) : base(GenerateUnsignedRegularExpression(min, max))
		{
		}

		protected AbstractNumericRegularExpressionTableLexerEntry(ulong min, ulong max) : base(GenerateUnsignedRegularExpression(min, max))
		{
		}

		protected AbstractNumericRegularExpressionTableLexerEntry(sbyte min, sbyte max, bool ignoreCase) : base(GenerateRegularExpression(min, max), ignoreCase)
		{
		}

		protected AbstractNumericRegularExpressionTableLexerEntry(short min, short max, bool ignoreCase) : base(GenerateRegularExpression(min, max), ignoreCase)
		{
		}

		protected AbstractNumericRegularExpressionTableLexerEntry(int min, int max, bool ignoreCase) : base(GenerateRegularExpression(min, max), ignoreCase)
		{
		}

		protected AbstractNumericRegularExpressionTableLexerEntry(long min, long max, bool ignoreCase) : base(GenerateRegularExpression(min, max), ignoreCase)
		{
		}

		protected AbstractNumericRegularExpressionTableLexerEntry(byte min, byte max, bool ignoreCase) : base(GenerateUnsignedRegularExpression(min, max), ignoreCase)
		{
		}

		protected AbstractNumericRegularExpressionTableLexerEntry(ushort min, ushort max, bool ignoreCase) : base(GenerateUnsignedRegularExpression(min, max), ignoreCase)
		{
		}

		protected AbstractNumericRegularExpressionTableLexerEntry(uint min, uint max, bool ignoreCase) : base(GenerateUnsignedRegularExpression(min, max), ignoreCase)
		{
		}

		protected AbstractNumericRegularExpressionTableLexerEntry(ulong min, ulong max, bool ignoreCase) : base(GenerateUnsignedRegularExpression(min, max), ignoreCase)
		{
		}
	}
}
