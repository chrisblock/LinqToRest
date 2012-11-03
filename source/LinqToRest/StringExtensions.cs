using System;

namespace LinqToRest
{
	public static class StringExtensions
	{
		public static string SubString(this string str, int start, int end)
		{
			var result = String.Empty;

			if ((end < start) && (end > -1))
			{
				throw new ArgumentException(String.Format("The end position ({0}) cannot be less than the start position ({1}).", end, start));
			}

			if (start != end)
			{
				var length = str.Length - start;

				if (end < 0)
				{
					length += end;
				}
				else if (end < str.Length)
				{
					length -= str.Length - end;
				}

				result = str.Substring(start, length);
			}

			return result;
		}
	}
}
