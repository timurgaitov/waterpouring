using System.Collections.Generic;
using System.Linq;
using WaterPouring.Moves;

namespace WaterPouring
{
	public interface IPouringGenerator
	{
		IEnumerable<Pouring> GetNextGeneration(Pouring pouring);
	}

	public class PouringGenerator : IPouringGenerator
	{
		private readonly IList<IMove> moves;
		private readonly ISet<Pouring> visited = new HashSet<Pouring>();

		public PouringGenerator(int numberOfCups)
		{
			var indexes = Enumerable
				.Range(0, numberOfCups)
				.ToArray();

			moves = indexes
				.Select(i => (IMove)new Empty(i))
				.Concat(indexes.Select(i => new Fill(i)))
				.Concat(indexes.SelectMany(i => indexes.Where(j => j != i), (i, j) => new Pour(i, j)))
				.ToArray();
		}

		public IEnumerable<Pouring> GetNextGeneration(Pouring pouring)
		{
			return moves
				.Where(m => m.CanPerform(pouring))
				.Select(m => m.Perform(pouring))
				.Where(p => !visited.Contains(p))
				.Select(p =>
				{
					visited.Add(p);
					return p;
				});
		}
	}
}