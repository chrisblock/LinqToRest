using System.Collections.Generic;

using LinqToRest.OData.Literals;

namespace LinqToRest
{
	public static class TokenStackExtensions
	{
		public static void Push(this Stack<Token> stack, TokenType tokenType, string value)
		{
			var token = new Token
			{
				TokenType = tokenType,
				Value = value
			};

			stack.Push(token);
		}
	}
}
