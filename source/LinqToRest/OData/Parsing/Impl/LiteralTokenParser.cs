using System;
using System.Collections.Generic;

using LinqToRest.OData.Filters;
using LinqToRest.OData.Literals;

namespace LinqToRest.OData.Parsing.Impl
{
	public class LiteralTokenParser : ILiteralTokenParser
	{
		private readonly IDictionary<TokenType, ILiteralTokenParser> _parsers;

		public LiteralTokenParser()
		{
			_parsers = new Dictionary<TokenType, ILiteralTokenParser>
			{
				{ TokenType.Boolean, new BooleanLiteralTokenParser() },
				{ TokenType.Byte, new ByteLiteralTokenParser() },
				{ TokenType.DateTime, new DateTimeLiteralTokenParser() },
				{ TokenType.DateTimeOffset, new DateTimeOffsetLiteralTokenParser() },
				{ TokenType.Decimal, new DecimalLiteralTokenParser() },
				{ TokenType.Double, new DoubleLiteralTokenParser() },
				{ TokenType.Float, new FloatLiteralTokenParser() },
				{ TokenType.Guid, new GuidLiteralTokenParser() },
				{ TokenType.Integer, new IntegerLiteralTokenParser() },
				{ TokenType.Long, new LongLiteralTokenParser() },
				{ TokenType.Name, new NameLiteralTokenParser() },
				{ TokenType.Null, new NullLiteralTokenParser() },
				{ TokenType.Short, new ShortLiteralTokenParser() },
				{ TokenType.String, new StringLiteralTokenParser() },
				{ TokenType.Time, new TimeLiteralTokenParser() },
				{ TokenType.Primitive, new PrimitiveLiteralTokenParser() }
			};
		}

		public FilterExpression Parse(Token token)
		{
			if (token == null)
			{
				throw new ArgumentNullException("token", "Cannot parse null literal token.");
			}

			ILiteralTokenParser parser;
			if (_parsers.TryGetValue(token.TokenType, out parser) == false)
			{
				throw new ArgumentException(String.Format("No ILiteralTokenParser registered for TokenType '{0}'.", token.TokenType));
			}

			return parser.Parse(token);
		}
	}
}
