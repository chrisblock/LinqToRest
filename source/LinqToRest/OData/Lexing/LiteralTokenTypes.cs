using System;
using System.Collections.Generic;

namespace LinqToRest.OData.Lexing
{
	public static class LiteralTokenTypes
	{
		private static readonly IDictionary<Type, TokenType> TokenTypeMap = new Dictionary<Type, TokenType>
		{
			{ typeof (bool),  TokenType.Boolean },
			{ typeof (byte),  TokenType.Byte },
			{ typeof (DateTimeOffset),  TokenType.DateTimeOffset },
			{ typeof (DateTime),  TokenType.DateTime },
			{ typeof (decimal),  TokenType.Decimal },
			{ typeof (double),  TokenType.Double },
			{ typeof (float),  TokenType.Float },
			{ typeof (Guid),  TokenType.Guid },
			{ typeof (int),  TokenType.Integer },
			{ typeof (long),  TokenType.Long },
			//{ typeof (Name),  TokenType.Name },
			//{ typeof (object),  TokenType.Null },
			{ typeof (short),  TokenType.Short },
			{ typeof (string),  TokenType.String },
			{ typeof (TimeSpan),  TokenType.Time }
		};

		public static TokenType Lookup(Type type)
		{
			TokenType result;

			TokenTypeMap.TryGetValue(type, out result);

			return result;
		}
	}
}
