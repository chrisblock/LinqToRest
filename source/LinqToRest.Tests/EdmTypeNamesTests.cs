// ReSharper disable InconsistentNaming

using System;

using LinqToRest.OData.Lexing;

using NUnit.Framework;

namespace LinqToRest.Tests
{
	[TestFixture]
	public class EdmTypeNamesTests
	{
		[Test]
		public void Lookup_Boolean_ReturnsEdmTypeName()
		{
			var type = typeof (bool);

			const string expected = "edm.boolean";
			var actual = EdmTypeNames.Lookup(type);

			Assert.That(expected, Is.EqualTo(actual));
		}

		[Test]
		public void Lookup_NullableBoolean_ReturnsEdmTypeName()
		{
			var type = typeof (bool?);

			const string expected = "edm.boolean";
			var actual = EdmTypeNames.Lookup(type);

			Assert.That(expected, Is.EqualTo(actual));
		}

		[Test]
		public void Lookup_Byte_ReturnsEdmTypeName()
		{
			var type = typeof (byte);

			const string expected = "edm.byte";
			var actual = EdmTypeNames.Lookup(type);

			Assert.That(expected, Is.EqualTo(actual));
		}

		[Test]
		public void Lookup_NullableByte_ReturnsEdmTypeName()
		{
			var type = typeof (byte?);

			const string expected = "edm.byte";
			var actual = EdmTypeNames.Lookup(type);

			Assert.That(expected, Is.EqualTo(actual));
		}

		[Test]
		public void Lookup_DateTime_ReturnsEdmTypeName()
		{
			var type = typeof (DateTime);

			const string expected = "edm.datetime";
			var actual = EdmTypeNames.Lookup(type);

			Assert.That(expected, Is.EqualTo(actual));
		}

		[Test]
		public void Lookup_NullableDateTime_ReturnsEdmTypeName()
		{
			var type = typeof (DateTime?);

			const string expected = "edm.datetime";
			var actual = EdmTypeNames.Lookup(type);

			Assert.That(expected, Is.EqualTo(actual));
		}

		[Test]
		public void Lookup_Decimal_ReturnsEdmTypeName()
		{
			var type = typeof (decimal);

			const string expected = "edm.decimal";
			var actual = EdmTypeNames.Lookup(type);

			Assert.That(expected, Is.EqualTo(actual));
		}

		[Test]
		public void Lookup_NullableDecimal_ReturnsEdmTypeName()
		{
			var type = typeof (decimal?);

			const string expected = "edm.decimal";
			var actual = EdmTypeNames.Lookup(type);

			Assert.That(expected, Is.EqualTo(actual));
		}

		[Test]
		public void Lookup_Double_ReturnsEdmTypeName()
		{
			var type = typeof (double);

			const string expected = "edm.double";
			var actual = EdmTypeNames.Lookup(type);

			Assert.That(expected, Is.EqualTo(actual));
		}

		[Test]
		public void Lookup_NullableDouble_ReturnsEdmTypeName()
		{
			var type = typeof (double?);

			const string expected = "edm.double";
			var actual = EdmTypeNames.Lookup(type);

			Assert.That(expected, Is.EqualTo(actual));
		}

		[Test]
		public void Lookup_Float_ReturnsEdmTypeName()
		{
			var type = typeof (float);

			const string expected = "edm.float";
			var actual = EdmTypeNames.Lookup(type);

			Assert.That(expected, Is.EqualTo(actual));
		}

		[Test]
		public void Lookup_NullableFloat_ReturnsEdmTypeName()
		{
			var type = typeof (float?);

			const string expected = "edm.float";
			var actual = EdmTypeNames.Lookup(type);

			Assert.That(expected, Is.EqualTo(actual));
		}

		[Test]
		public void Lookup_Guid_ReturnsEdmTypeName()
		{
			var type = typeof (Guid);

			const string expected = "edm.guid";
			var actual = EdmTypeNames.Lookup(type);

			Assert.That(expected, Is.EqualTo(actual));
		}

		[Test]
		public void Lookup_NullableGuid_ReturnsEdmTypeName()
		{
			var type = typeof (Guid?);

			const string expected = "edm.guid";
			var actual = EdmTypeNames.Lookup(type);

			Assert.That(expected, Is.EqualTo(actual));
		}

		[Test]
		public void Lookup_Short_ReturnsEdmTypeName()
		{
			var type = typeof (short);

			const string expected = "edm.int16";
			var actual = EdmTypeNames.Lookup(type);

			Assert.That(expected, Is.EqualTo(actual));
		}

		[Test]
		public void Lookup_NullableShort_ReturnsEdmTypeName()
		{
			var type = typeof (short?);

			const string expected = "edm.int16";
			var actual = EdmTypeNames.Lookup(type);

			Assert.That(expected, Is.EqualTo(actual));
		}

		[Test]
		public void Lookup_Integer_ReturnsEdmTypeName()
		{
			var type = typeof (int);

			const string expected = "edm.int32";
			var actual = EdmTypeNames.Lookup(type);

			Assert.That(expected, Is.EqualTo(actual));
		}

		[Test]
		public void Lookup_NullableInteger_ReturnsEdmTypeName()
		{
			var type = typeof (int?);

			const string expected = "edm.int32";
			var actual = EdmTypeNames.Lookup(type);

			Assert.That(expected, Is.EqualTo(actual));
		}

		[Test]
		public void Lookup_Long_ReturnsEdmTypeName()
		{
			var type = typeof (long);

			const string expected = "edm.int64";
			var actual = EdmTypeNames.Lookup(type);

			Assert.That(expected, Is.EqualTo(actual));
		}

		[Test]
		public void Lookup_NullableLong_ReturnsEdmTypeName()
		{
			var type = typeof (long?);

			const string expected = "edm.int64";
			var actual = EdmTypeNames.Lookup(type);

			Assert.That(expected, Is.EqualTo(actual));
		}

		[Test]
		public void Lookup_SignedByte_ReturnsEdmTypeName()
		{
			var type = typeof (sbyte);

			const string expected = "edm.sbyte";
			var actual = EdmTypeNames.Lookup(type);

			Assert.That(expected, Is.EqualTo(actual));
		}

		[Test]
		public void Lookup_NullableSignedByte_ReturnsEdmTypeName()
		{
			var type = typeof (sbyte?);

			const string expected = "edm.sbyte";
			var actual = EdmTypeNames.Lookup(type);

			Assert.That(expected, Is.EqualTo(actual));
		}

		[Test]
		public void Lookup_String_ReturnsEdmTypeName()
		{
			var type = typeof (string);

			const string expected = "edm.string";
			var actual = EdmTypeNames.Lookup(type);

			Assert.That(expected, Is.EqualTo(actual));
		}

		[Test]
		public void Lookup_TimeSpan_ReturnsEdmTypeName()
		{
			var type = typeof (TimeSpan);

			const string expected = "edm.time";
			var actual = EdmTypeNames.Lookup(type);

			Assert.That(expected, Is.EqualTo(actual));
		}

		[Test]
		public void Lookup_NullableTimeSpan_ReturnsEdmTypeName()
		{
			var type = typeof (TimeSpan?);

			const string expected = "edm.time";
			var actual = EdmTypeNames.Lookup(type);

			Assert.That(expected, Is.EqualTo(actual));
		}

		[Test]
		public void Lookup_DateTimeOffset_ReturnsEdmTypeName()
		{
			var type = typeof (DateTimeOffset);

			const string expected = "edm.datetimeoffset";
			var actual = EdmTypeNames.Lookup(type);

			Assert.That(expected, Is.EqualTo(actual));
		}

		[Test]
		public void Lookup_NullableDateTimeOffset_ReturnsEdmTypeName()
		{
			var type = typeof (DateTimeOffset?);

			const string expected = "edm.datetimeoffset";
			var actual = EdmTypeNames.Lookup(type);

			Assert.That(expected, Is.EqualTo(actual));
		}

		[Test]
		public void Lookup_BooleanEdmTypeName_ReturnsBoolean()
		{
			const string edmTypeName = "edm.boolean";

			var expected = typeof (bool);
			var actual = EdmTypeNames.Lookup(edmTypeName);

			Assert.That(expected, Is.EqualTo(actual));
		}

		[Test]
		public void Lookup_ByteEdmTypeName_ReturnsByte()
		{
			const string edmTypeName = "edm.byte";

			var expected = typeof (byte);
			var actual = EdmTypeNames.Lookup(edmTypeName);

			Assert.That(expected, Is.EqualTo(actual));
		}

		[Test]
		public void Lookup_DateTimeEdmTypeName_ReturnsDateTime()
		{
			const string edmTypeName = "edm.datetime";

			var expected = typeof (DateTime);
			var actual = EdmTypeNames.Lookup(edmTypeName);

			Assert.That(expected, Is.EqualTo(actual));
		}

		[Test]
		public void Lookup_DecimalEdmTypeName_ReturnsDecimal()
		{
			const string edmTypeName = "edm.decimal";

			var expected = typeof (decimal);
			var actual = EdmTypeNames.Lookup(edmTypeName);

			Assert.That(expected, Is.EqualTo(actual));
		}

		[Test]
		public void Lookup_DoubleEdmTypeName_ReturnsDouble()
		{
			const string edmTypeName = "edm.double";

			var expected = typeof (double);
			var actual = EdmTypeNames.Lookup(edmTypeName);

			Assert.That(expected, Is.EqualTo(actual));
		}

		[Test]
		public void Lookup_FloatEdmTypeName_ReturnsFloat()
		{
			const string edmTypeName = "edm.float";

			var expected = typeof (float);
			var actual = EdmTypeNames.Lookup(edmTypeName);

			Assert.That(expected, Is.EqualTo(actual));
		}

		[Test]
		public void Lookup_SingleEdmTypeName_ReturnsFloat()
		{
			const string edmTypeName = "edm.single";

			var expected = typeof (float);
			var actual = EdmTypeNames.Lookup(edmTypeName);

			Assert.That(expected, Is.EqualTo(actual));
		}

		[Test]
		public void Lookup_GuidEdmTypeName_ReturnsGuid()
		{
			const string edmTypeName = "edm.guid";

			var expected = typeof (Guid);
			var actual = EdmTypeNames.Lookup(edmTypeName);

			Assert.That(expected, Is.EqualTo(actual));
		}

		[Test]
		public void Lookup_Int16EdmTypeName_ReturnsShort()
		{
			const string edmTypeName = "edm.int16";

			var expected = typeof (short);
			var actual = EdmTypeNames.Lookup(edmTypeName);

			Assert.That(expected, Is.EqualTo(actual));
		}

		[Test]
		public void Lookup_Int32EdmTypeName_ReturnsInteger()
		{
			const string edmTypeName = "edm.int32";

			var expected = typeof (int);
			var actual = EdmTypeNames.Lookup(edmTypeName);

			Assert.That(expected, Is.EqualTo(actual));
		}

		[Test]
		public void Lookup_Int64EdmTypeName_ReturnsLong()
		{
			const string edmTypeName = "edm.int64";

			var expected = typeof (long);
			var actual = EdmTypeNames.Lookup(edmTypeName);

			Assert.That(expected, Is.EqualTo(actual));
		}

		[Test]
		public void Lookup_SignedByteEdmTypeName_ReturnsSignedByte()
		{
			const string edmTypeName = "edm.sbyte";

			var expected = typeof (sbyte);
			var actual = EdmTypeNames.Lookup(edmTypeName);

			Assert.That(expected, Is.EqualTo(actual));
		}

		[Test]
		public void Lookup_StringEdmTypeName_ReturnsString()
		{
			const string edmTypeName = "edm.string";

			var expected = typeof (string);
			var actual = EdmTypeNames.Lookup(edmTypeName);

			Assert.That(expected, Is.EqualTo(actual));
		}

		[Test]
		public void Lookup_TimeEdmTypeName_ReturnsTimeSpan()
		{
			const string edmTypeName = "edm.time";

			var expected = typeof (TimeSpan);
			var actual = EdmTypeNames.Lookup(edmTypeName);

			Assert.That(expected, Is.EqualTo(actual));
		}

		[Test]
		public void Lookup_DateTimeOffsetEdmTypeName_ReturnsDateTimeOffset()
		{
			const string edmTypeName = "edm.datetimeoffset";

			var expected = typeof (DateTimeOffset);
			var actual = EdmTypeNames.Lookup(edmTypeName);

			Assert.That(expected, Is.EqualTo(actual));
		}

	}
}
