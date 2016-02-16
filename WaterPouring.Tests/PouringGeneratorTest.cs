using System.Linq;
using NUnit.Framework;
using WaterPouring.Moves;

namespace WaterPouring.Tests
{
	[TestFixture]
	public class PouringGeneratorTest
	{
		[Test]
		public void Test_next_generation()
		{
			var capacities = new[] {5, 3};

			var generator = new PouringGenerator(capacities.Length);

			var pouring = new Pouring(capacities, new[] {4, 2}, null, null);
			var generation = generator.GetNextGeneration(pouring).ToArray();

			var expectedGeneration = new[]
			{
				new Pouring(capacities, new[] {0, 2}, pouring, new Empty(0)),
				new Pouring(capacities, new[] {4, 0}, pouring, new Empty(1)),
				new Pouring(capacities, new[] {capacities[0], 2}, pouring, new Fill(0)),
				new Pouring(capacities, new[] {4, capacities[1]}, pouring, new Fill(1)),
				new Pouring(capacities, new[] {3, 3}, pouring, new Pour(0, 1)),
				new Pouring(capacities, new[] {5, 1}, pouring, new Pour(1, 0)),
			};

			CollectionAssert.AreEquivalent(expectedGeneration, generation);
		}

		[Test]
		public void Test_can_empty_and_can_pour()
		{
			var capacities = new[] {5, 3};

			var generator = new PouringGenerator(capacities.Length);

			var pouring = new Pouring(capacities, new[] {0, 2}, null, null);
			var generation = generator.GetNextGeneration(pouring).ToArray();

			var expectedGeneration = new[]
			{
				new Pouring(capacities, new[] {0, 0}, pouring, new Empty(1)),
				new Pouring(capacities, new[] {capacities[0], 2}, pouring, new Fill(0)),
				new Pouring(capacities, new[] {0, capacities[1]}, pouring, new Fill(1)),
				new Pouring(capacities, new[] {2, 0}, pouring, new Pour(1, 0)),
			};

			CollectionAssert.AreEquivalent(expectedGeneration, generation);
		}
	}
}