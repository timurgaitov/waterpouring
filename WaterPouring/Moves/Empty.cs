using System;
using System.Linq;

namespace WaterPouring.Moves
{
	public class Empty : IMove
	{
		private readonly int index;

		public Empty(int index)
		{
			this.index = index;
		}

		public bool CanPerform(Pouring pouring)
		{
			return pouring.State[index] > 0;
		}

		public Pouring Perform(Pouring pouring)
		{
			if (!CanPerform(pouring))
			{
				throw new InvalidOperationException();
			}

			var stateCopy = pouring.State.ToArray();
			stateCopy[index] = 0;
			return new Pouring(pouring.Capacities, stateCopy, pouring, this);
		}

		public override string ToString()
		{
			return $"Empty {index}";
		}
	}
}