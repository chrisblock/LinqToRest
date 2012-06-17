using System;
using System.Collections.Generic;
using System.Threading;

namespace LinqToRest.OData.Filters
{
	public class BestFitTypeDeterminer
	{
		private static readonly Lazy<IDictionary<Type, int>> LazyTypeWeights = new Lazy<IDictionary<Type, int>>(BuildWeightDictionary, LazyThreadSafetyMode.ExecutionAndPublication);
		private static IDictionary<Type, int> TypeWeights { get { return LazyTypeWeights.Value; } }

		private static readonly Lazy<IDictionary<int, Type>> LazyIntegralNextSizeUp = new Lazy<IDictionary<int, Type>>(BuildIntegralNextSizeUpDictionary, LazyThreadSafetyMode.ExecutionAndPublication);
		private static IDictionary<int, Type> IntegralNextSizeUp { get { return LazyIntegralNextSizeUp.Value; } }

		private static readonly Lazy<IDictionary<int, Type>> LazyFloatingPointNextSizeUp = new Lazy<IDictionary<int, Type>>(BuildFloatingPointNextSizeUpDictionary, LazyThreadSafetyMode.ExecutionAndPublication);
		private static IDictionary<int, Type> FloatingPointNextSizeUp { get { return LazyFloatingPointNextSizeUp.Value; } }

		private static IDictionary<Type, int> BuildWeightDictionary()
		{
			var result = new Dictionary<Type, int>
			{
				{ typeof (sbyte), sizeof (sbyte) + (IsUnsigned(typeof (sbyte)) ? 1 : 0) },
				{ typeof (byte), sizeof (byte) + (IsUnsigned(typeof (byte)) ? 1 : 0) },
				{ typeof (short), sizeof (short) + (IsUnsigned(typeof (short)) ? 1 : 0) },
				{ typeof (ushort), sizeof (ushort) + (IsUnsigned(typeof (ushort)) ? 1 : 0) },
				{ typeof (int), sizeof (int) + (IsUnsigned(typeof (int)) ? 1 : 0) },
				{ typeof (uint), sizeof (uint) + (IsUnsigned(typeof (uint)) ? 1 : 0) },
				{ typeof (long), sizeof (long) + (IsUnsigned(typeof (long)) ? 1 : 0) },
				{ typeof (ulong), sizeof (ulong) + (IsUnsigned(typeof (ulong)) ? 1 : 0) },
				{ typeof (float), sizeof (float) + (IsUnsigned(typeof (float)) ? 1 : 0) },
				{ typeof (double), sizeof (double) + (IsUnsigned(typeof (double)) ? 1 : 0) },
				{ typeof (decimal), sizeof (decimal) + (IsUnsigned(typeof (decimal)) ? 1 : 0) }
			};

			return result;
		}

		private static IDictionary<int, Type> BuildIntegralNextSizeUpDictionary()
		{
			return new Dictionary<int, Type>
			{
				{ sizeof (short), typeof (short) },
				{ sizeof (int), typeof (int) },
				{ sizeof (long), typeof (long) }
			};
		}

		private static IDictionary<int, Type> BuildFloatingPointNextSizeUpDictionary()
		{
			return new Dictionary<int, Type>
			{
				{ sizeof (double), typeof (double) },
				{ sizeof (decimal), typeof (decimal) }
			};
		}

		private static bool IsUnsigned(Type type)
		{
			return ((type == typeof(byte)) || (type == typeof(ushort)) || (type == typeof(uint)) || (type == typeof(ulong)));
		}

		private static bool IsFloatingPointType(Type type)
		{
			return ((type == typeof (float)) || (type == typeof (double)) || (type == typeof (decimal)));
		}

		public Type DetermineBestFit(Type x, Type y)
		{
			if (x == null)
			{
				throw new ArgumentNullException("x", "Cannot compare with a null type.");
			}

			if (y == null)
			{
				throw new ArgumentNullException("y", "Cannot compare with a null type.");
			}

			Type result;

			if (IsFloatingPointType(x) || IsFloatingPointType(y))
			{
				result = DetermineBestFloatingPointFit(x, y);
			}
			else
			{
				result = DetermineBestIntegralFit(x, y);
			}

			return result;
		}

		private static Type DetermineBestFloatingPointFit(Type x, Type y)
		{
			return DetermineBestFit(FloatingPointNextSizeUp, x, y);
		}

		private static Type DetermineBestIntegralFit(Type x, Type y)
		{
			return DetermineBestFit(IntegralNextSizeUp, x, y);
		}

		private static Type DetermineBestFit(IDictionary<int, Type> nextSizeUp, Type x, Type y)
		{
			var result = x;

			int xWeight, yWeight;

			if (TypeWeights.TryGetValue(x, out xWeight) == false)
			{
				throw new ArgumentException(String.Format("Cannot determine best fit for type '{0}'.", x));
			}

			if (TypeWeights.TryGetValue(y, out yWeight) == false)
			{
				throw new ArgumentException(String.Format("Cannot determine best fit for type '{0}'.", y));
			}

			var difference = Math.Abs(xWeight - yWeight);

			if (difference > 1)
			{
				if (xWeight < yWeight)
				{
					result = y;
				}
			}
			else if (difference != 0)
			{
				var max = Math.Max(xWeight, yWeight);
				var log2 = Math.Ceiling(Math.Log(max) / Math.Log(2));

				var size = Math.Pow(2, log2);

				if (nextSizeUp.TryGetValue((int)size, out result) == false)
				{
					result = DetermineBestFloatingPointFit(x, y);
				}
			}

			return result;
		}
	}
}
