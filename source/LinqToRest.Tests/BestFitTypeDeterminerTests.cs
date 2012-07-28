// ReSharper disable InconsistentNaming

using System;

using LinqToRest.OData.Filters;

using NUnit.Framework;

namespace LinqToRest.Tests
{
	[TestFixture]
	public class BestFitTypeDeterminerTests
	{
		private static readonly BestFitTypeDeterminer Determiner = new BestFitTypeDeterminer();

		[Test]
		public void DetermineBestFit_NullAndInteger_ThrowsArgumentNullException()
		{
			Type x = null;
			Type y = typeof (int);

			Assert.That(() => Determiner.DetermineBestFit(x, y), Throws.InstanceOf<ArgumentNullException>());
		}

		[Test]
		public void DetermineBestFit_ByteAndNull_ThrowsArgumentNullException()
		{
			Type x = typeof (byte);
			Type y = null;

			Assert.That(() => Determiner.DetermineBestFit(x, y), Throws.InstanceOf<ArgumentNullException>());
		}

		[Test]
		public void DetermineBestFit_StringAndInteger_ThrowsArgumentException()
		{
			Type x = typeof (string);
			Type y = typeof (int);

			Assert.That(() => Determiner.DetermineBestFit(x, y), Throws.ArgumentException);
		}

		[Test]
		public void DetermineBestFit_IntegerAndString_ThrowsArgumentException()
		{
			Type x = typeof (int);
			Type y = typeof (string);

			Assert.That(() => Determiner.DetermineBestFit(x, y), Throws.ArgumentException);
		}

		[Test]
		public void DetermineBestFit_ByteAndDecimal_ReturnsDecimal()
		{
			Type x = typeof (byte);
			Type y = typeof (decimal);

			Assert.That(Determiner.DetermineBestFit(x, y), Is.EqualTo(typeof (decimal)));
		}

		[Test]
		public void DetermineBestFit_ShortAndDecimal_ReturnsDecimal()
		{
			Type x = typeof (short);
			Type y = typeof (decimal);

			Assert.That(Determiner.DetermineBestFit(x, y), Is.EqualTo(typeof (decimal)));
		}

		[Test]
		public void DetermineBestFit_IntegerAndDecimal_ReturnsDecimal()
		{
			Type x = typeof (int);
			Type y = typeof (decimal);

			Assert.That(Determiner.DetermineBestFit(x, y), Is.EqualTo(typeof (decimal)));
		}

		[Test]
		public void DetermineBestFit_LongAndDecimal_ReturnsDecimal()
		{
			Type x = typeof (long);
			Type y = typeof (decimal);

			Assert.That(Determiner.DetermineBestFit(x, y), Is.EqualTo(typeof (decimal)));
		}

		[Test]
		public void DetermineBestFit_FloatAndDecimal_ReturnsDecimal()
		{
			Type x = typeof (float);
			Type y = typeof (decimal);

			Assert.That(Determiner.DetermineBestFit(x, y), Is.EqualTo(typeof (decimal)));
		}

		[Test]
		public void DetermineBestFit_DoubleAndDecimal_ReturnsDecimal()
		{
			Type x = typeof (double);
			Type y = typeof (decimal);

			Assert.That(Determiner.DetermineBestFit(x, y), Is.EqualTo(typeof (decimal)));
		}

		[Test]
		public void DetermineBestFit_DecimalAndDecimal_ReturnsDecimal()
		{
			Type x = typeof (decimal);
			Type y = typeof (decimal);

			Assert.That(Determiner.DetermineBestFit(x, y), Is.EqualTo(typeof (decimal)));
		}

		[Test]
		public void DetermineBestFit_DecimalAndByte_ReturnsDecimal()
		{
			Type x = typeof (decimal);
			Type y = typeof (byte);

			Assert.That(Determiner.DetermineBestFit(x, y), Is.EqualTo(typeof (decimal)));
		}

		[Test]
		public void DetermineBestFit_DecimalAndShort_ReturnsDecimal()
		{
			Type x = typeof (decimal);
			Type y = typeof (short);

			Assert.That(Determiner.DetermineBestFit(x, y), Is.EqualTo(typeof (decimal)));
		}

		[Test]
		public void DetermineBestFit_DecimalAndInteger_ReturnsDecimal()
		{
			Type x = typeof (decimal);
			Type y = typeof (int);

			Assert.That(Determiner.DetermineBestFit(x, y), Is.EqualTo(typeof (decimal)));
		}

		[Test]
		public void DetermineBestFit_DecimalAndLong_ReturnsDecimal()
		{
			Type x = typeof (decimal);
			Type y = typeof (long);

			Assert.That(Determiner.DetermineBestFit(x, y), Is.EqualTo(typeof (decimal)));
		}

		[Test]
		public void DetermineBestFit_DecimalAndFloat_ReturnsDecimal()
		{
			Type x = typeof (decimal);
			Type y = typeof (float);

			Assert.That(Determiner.DetermineBestFit(x, y), Is.EqualTo(typeof (decimal)));
		}

		[Test]
		public void DetermineBestFit_DecimalAndDouble_ReturnsDecimal()
		{
			Type x = typeof (decimal);
			Type y = typeof (double);

			Assert.That(Determiner.DetermineBestFit(x, y), Is.EqualTo(typeof (decimal)));
		}

		[Test]
		public void DetermineBestFit_IntegerAndUnsignedInteger_ReturnsLong()
		{
			Type x = typeof (int);
			Type y = typeof (uint);

			Assert.That(Determiner.DetermineBestFit(x, y), Is.EqualTo(typeof (long)));
		}

		[Test]
		public void DetermineBestFit_LongAndUnsignedLong_ReturnsDecimal()
		{
			Type x = typeof (long);
			Type y = typeof (ulong);

			Assert.That(Determiner.DetermineBestFit(x, y), Is.EqualTo(typeof (decimal)));
		}

		[Test]
		public void DetermineBestFit_IntegerAndDouble_ReturnsDouble()
		{
			Type x = typeof (int);
			Type y = typeof (double);

			Assert.That(Determiner.DetermineBestFit(x, y), Is.EqualTo(typeof (double)));
		}
	}
}
