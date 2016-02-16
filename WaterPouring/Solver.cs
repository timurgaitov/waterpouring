using System;
using System.Collections.Generic;
using System.Linq;

namespace WaterPouring
{
	public interface ISolver
	{
		IEnumerable<Solution> SolutionSequence(int value, params int[] capacities);
	}

	public class Solver : ISolver
	{
		private readonly IPouringGeneratorFactory pouringGeneratorFactory;

		public Solver(
			IPouringGeneratorFactory pouringGeneratorFactory)
		{
			this.pouringGeneratorFactory = pouringGeneratorFactory;
		}

		public IEnumerable<Solution> SolutionSequence(int value, params int[] capacities)
		{
			if (capacities.All(c => value > c))
			{
				yield break;
			}

			var pouringGenerator = pouringGeneratorFactory.Create(capacities.Length);

			var queue = new Queue<Pouring>();
			queue.Enqueue(new Pouring(capacities));

			while (true)
			{
				if (queue.Count == 0)
				{
					break;
				}

				var pouring = queue.Dequeue();

				if (pouring.State.Contains(value))
				{
					yield return new Solution(pouring);
				}
				else
				{
					foreach (var p in pouringGenerator.GetNextGeneration(pouring))
					{
						queue.Enqueue(p);
					}
				}
			}
			

		}
	}
}