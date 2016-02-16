using System;
using System.Collections.Generic;

namespace WaterPouring
{
	/// <see cref="http://stackoverflow.com/questions/7244699/gethashcode-on-byte-array"/>
	/// <typeparam name="T"></typeparam>
	internal sealed class ArrayEqualityComparer<T> : IEqualityComparer<IReadOnlyList<T>>
	{
		// You could make this a per-instance field with a constructor parameter
		private static readonly EqualityComparer<T> elementComparer
			= EqualityComparer<T>.Default;

		public bool Equals(IReadOnlyList<T> first, IReadOnlyList<T> second)
		{
			if (object.Equals(first, second))
			{
				return true;
			}
			if (first == null || second == null)
			{
				return false;
			}
			if (first.Count != second.Count)
			{
				return false;
			}
			for (int i = 0; i < first.Count; i++)
			{
				if (!elementComparer.Equals(first[i], second[i]))
				{
					return false;
				}
			}
			return true;
		}

		public int GetHashCode(IReadOnlyList<T> array)
		{
			unchecked
			{
				if (array == null)
				{
					return 0;
				}
				int hash = 17;
				foreach (T element in array)
				{
					hash = hash * 31 + elementComparer.GetHashCode(element);
				}
				return hash;
			}
		}
	}
}