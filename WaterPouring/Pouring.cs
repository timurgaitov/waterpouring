using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using WaterPouring.Moves;

namespace WaterPouring
{
	public class Pouring
	{
		private static readonly IEqualityComparer<IReadOnlyList<int>> equalityComparer = new ArrayEqualityComparer<int>();

		public IReadOnlyList<int> Capacities { get; }
		public IReadOnlyList<int> State { get; }
		public Pouring Previous { get; }
		public IMove Move { get; }

		public Pouring(params int[] capacities) : this(
			new ReadOnlyCollection<int>(capacities.ToArray()), 
			new ReadOnlyCollection<int>(new int[capacities.Length]), 
			null, 
			null)
		{
		}

		internal Pouring(IReadOnlyList<int> capacities, IReadOnlyList<int> state, Pouring previous, IMove move)
		{
			Capacities = capacities;
			State = state;
			Move = move;
			Previous = previous;
		}

		public override bool Equals(object obj)
		{
			return equalityComparer.Equals(State, ((Pouring)obj).State);
		}

		public override int GetHashCode()
		{
			return equalityComparer.GetHashCode(State);
		}

		public override string ToString()
		{
			return string.Join(", ", Capacities.Select((c, i) => $"{State[i]}/{c}"));
		}
	}
}