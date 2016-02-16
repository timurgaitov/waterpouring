using System;
using System.Linq;

namespace WaterPouring.Moves
{
	public class Fill : IMove
	{
		private readonly int index;

		public Fill(int index)
		{
			this.index = index;
		}

		public bool CanPerform(Pouring pouring)
		{
			return true;
		}

		public Pouring Perform(Pouring pouring)
		{
			if (!CanPerform(pouring))
			{
				throw new InvalidOperationException();
			}

			var stateCopy = pouring.State.ToArray();
			stateCopy[index] = pouring.Capacities[index];
			return new Pouring(pouring.Capacities, stateCopy, pouring, this);
		}

		public override string ToString()
		{
			return $"Fill {index}";
		}
	}
}