using System;
using System.Linq;

namespace WaterPouring.Moves
{
	public class Pour : IMove
	{
		private readonly int @from;
		private readonly int to;

		public Pour(int @from, int to)
		{
			this.@from = @from;
			this.to = to;
		}

		public bool CanPerform(Pouring pouring)
		{
			return pouring.State[@from] > 0
				&& pouring.State[to] < pouring.Capacities[to];
		}

		public Pouring Perform(Pouring pouring)
		{
			if(!CanPerform(pouring))
			{
				throw new InvalidOperationException();
			}

			var pourAmount = Math.Min(pouring.State[@from], pouring.Capacities[to] - pouring.State[to]);

			var stateCopy = pouring.State.ToArray();

			stateCopy[@from] = stateCopy[@from] - pourAmount;
			stateCopy[to] = stateCopy[to] + pourAmount;

			return new Pouring(pouring.Capacities, stateCopy, pouring, this);
		}

		public override string ToString()
		{
			return $"Pour from {@from} to {to}";
		}
	}
}