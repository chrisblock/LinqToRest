using System;
using System.Collections.Generic;

namespace LinqToRest.OData.Filters
{
	public class TypeComparer : IComparer<Type>
	{
		private static readonly IDictionary<Type, int> TypeWeights = new Dictionary<Type, int>
		{
			{ typeof(byte), 0 },
			{ typeof(short), 1 },
			{ typeof(int), 2 },
			{ typeof(long), 3 },
			{ typeof(float), 4 },
			{ typeof(double), 5 },
			{ typeof(decimal), 6 }
		};

		public int Compare(Type x, Type y)
		{
			int xWeight, yWeight;

			if (TypeWeights.TryGetValue(x, out xWeight) == false)
			{
				throw new ArgumentException(String.Format("Unable to compare type '{0}' and type '{1}'.", x, y));
			}

			if (TypeWeights.TryGetValue(y, out yWeight) == false)
			{
				throw new ArgumentException(String.Format("Unable to compare type '{0}' and type '{1}'.", x, y));
			}

			return xWeight - yWeight;
		}
	}
}
