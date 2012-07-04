// ReSharper disable InconsistentNaming

using System;
using System.Security.Cryptography;

using LinqToRest.OData.Lexing;
using LinqToRest.OData.Lexing.Impl;

using NUnit.Framework;

namespace LinqToRest.Tests.Lexing
{
	[TestFixture]
	public class DecimalRegularExpressionTableLexerEntryTests
	{
		private IRegularExpressionTableLexerEntry _regularExpressionTableLexerEntry;

		private static string BuildLiteralString()
		{
			var result = String.Format("{0}m", RandomDecimal());

			Console.WriteLine("Generated decimal: '{0}'.", result);

			return result;
		}

		private static decimal RandomDecimal()
		{
			var rng = RandomNumberGenerator.Create();

			var ints = new int[3];
			var bytes = new byte[14];
			rng.GetBytes(bytes);

			for (var i = 0; i < ints.Length; i++)
			{
				ints[i] = BitConverter.ToInt32(bytes, sizeof(int) * i);
			}

			return new Decimal(ints[0], ints[1], ints[2], BitConverter.ToBoolean(bytes, 12), (byte)(bytes[13] % 29));
		}

		[SetUp]
		public void TestSetUp()
		{
			_regularExpressionTableLexerEntry = new DecimalRegularExpressionTableLexerEntry();
		}

		[Test]
		public void IsContainedIn_ValidDecimal_ReturnsTrue()
		{
			var result = _regularExpressionTableLexerEntry.IsContainedIn(String.Format("{0}", BuildLiteralString()));

			Assert.That(result, Is.True);
		}

		[Test]
		public void IsContainedIn_ValidDecimalFlushWithText_ReturnsFalse()
		{
			var result = _regularExpressionTableLexerEntry.IsContainedIn(String.Format("hello world{0}", BuildLiteralString()));

			Assert.That(result, Is.False);
		}

		[Test]
		public void IsAtStart_ValidDecimalAtStart_ReturnsTrue()
		{
			var result = _regularExpressionTableLexerEntry.IsAtStart(String.Format("{0} hello world", BuildLiteralString()));

			Assert.That(result, Is.True);
		}

		[Test]
		public void IsAtStart_ValidDecimalFlushWithTextAtStart_ReturnsFalse()
		{
			var result = _regularExpressionTableLexerEntry.IsAtStart(String.Format("{0}hello world", BuildLiteralString()));

			Assert.That(result, Is.False);
		}

		[Test]
		public void IsAtStart_ValidDecimalFlushWithParenthesisAtStart_ReturnsTrue()
		{
			var result = _regularExpressionTableLexerEntry.IsAtStart(String.Format("{0})", BuildLiteralString()));

			Assert.That(result, Is.True);
		}

		[Test]
		public void IsAtStart_ValidDecimalNotAtStart_ReturnsFalse()
		{
			var result = _regularExpressionTableLexerEntry.IsAtStart(String.Format("hello world {0}", BuildLiteralString()));

			Assert.That(result, Is.False);
		}

		[Test]
		public void MatchesEntireText_ValidDecimal_ReturnsTrue()
		{
			var result = _regularExpressionTableLexerEntry.MatchesEntireText(String.Format("{0}", BuildLiteralString()));

			Assert.That(result, Is.True);
		}

		[Test]
		public void MatchesEntireText_TextBeforeValidDecimal_ReturnsFalse()
		{
			var result = _regularExpressionTableLexerEntry.MatchesEntireText(String.Format("hello world {0}", BuildLiteralString()));

			Assert.That(result, Is.False);
		}

		[Test]
		public void MatchesEntireText_TextAfterValidDecimal_ReturnsFalse()
		{
			var result = _regularExpressionTableLexerEntry.MatchesEntireText(String.Format("{0} hello world", BuildLiteralString()));

			Assert.That(result, Is.False);
		}
	}
}
