// ReSharper disable InconsistentNaming

using System;
using System.Linq;
using System.Security.Cryptography;

using LinqToRest.OData.Lexing;
using LinqToRest.Server.OData.Lexing;
using LinqToRest.Server.OData.Lexing.Impl;

using NUnit.Framework;

namespace LinqToRest.Server.Tests.Lexing
{
	[TestFixture]
	public class FunctionRegularExpressionTableLexerEntryTests
	{
		private static readonly string[] Functions = FunctionRegularExpressionTableLexerEntry.Functions.ToArray();

		private IRegularExpressionTableLexerEntry _regularExpressionTableLexerEntry;

		private static string BuildLiteralString()
		{
			var result = String.Format("{0}", RandomFunction());

			Console.WriteLine("Generated function: '{0}'.", result);

			return result;
		}

		private static string RandomFunction()
		{
			var rng = RandomNumberGenerator.Create();

			var data = new byte[4];
			rng.GetBytes(data);

			var index = BitConverter.ToInt32(data, 0) % Functions.Length;

			while (index < 0)
			{
				index += Functions.Length;
			}

			return Functions[index];
		}

		[SetUp]
		public void TestSetUp()
		{
			_regularExpressionTableLexerEntry = new FunctionRegularExpressionTableLexerEntry();
		}

		[Test]
		public void TokenType_ReturnsFunction()
		{
			Assert.That(_regularExpressionTableLexerEntry.TokenType, Is.EqualTo(TokenType.Function));
		}

		[Test]
		public void IsContainedIn_ValidFunction_ReturnsTrue()
		{
			var result = _regularExpressionTableLexerEntry.IsContainedIn(String.Format("{0}", BuildLiteralString()));

			Assert.That(result, Is.True);
		}

		[Test]
		public void IsContainedIn_ValidFunctionFlushWithTextAtEnd_ReturnsFalse()
		{
			var result = _regularExpressionTableLexerEntry.IsContainedIn(String.Format("hello world{0}", BuildLiteralString()));

			Assert.That(result, Is.False);
		}

		[Test]
		public void IsAtStart_ValidFunctionAtStart_ReturnsTrue()
		{
			var result = _regularExpressionTableLexerEntry.IsAtStart(String.Format("{0} hello world", BuildLiteralString()));

			Assert.That(result, Is.True);
		}

		[Test]
		public void IsAtStart_ValidFunctionFlushWithTextAtStart_ReturnsFalse()
		{
			var result = _regularExpressionTableLexerEntry.IsAtStart(String.Format("{0}hello world", BuildLiteralString()));

			Assert.That(result, Is.False);
		}

		[Test]
		public void IsAtStart_ValidFunctionNotAtStart_ReturnsFalse()
		{
			var result = _regularExpressionTableLexerEntry.IsAtStart(String.Format("hello world {0}", BuildLiteralString()));

			Assert.That(result, Is.False);
		}

		[Test]
		public void MatchesEntireText_ValidFunction_ReturnsTrue()
		{
			var result = _regularExpressionTableLexerEntry.MatchesEntireText(String.Format("{0}", BuildLiteralString()));

			Assert.That(result, Is.True);
		}

		[Test]
		public void MatchesEntireText_TextBeforeValidFunction_ReturnsFalse()
		{
			var result = _regularExpressionTableLexerEntry.MatchesEntireText(String.Format("hello world {0}", BuildLiteralString()));

			Assert.That(result, Is.False);
		}

		[Test]
		public void MatchesEntireText_TextAfterValidFunction_ReturnsFalse()
		{
			var result = _regularExpressionTableLexerEntry.MatchesEntireText(String.Format("{0} hello world", BuildLiteralString()));

			Assert.That(result, Is.False);
		}
	}
}
