using System.Linq;
using NUnit.Framework;
using Rhino.Mocks;

namespace WaterPouring.Tests
{
	[TestFixture]
	public class SolverTest
	{
		[Test]
		[TestCase(4, 3, 5)]
		[TestCase(6, 3, 5, 7)]
		public void Test_solution_found(int value, params int[] capacities)
		{
			var pouringGeneratorFactory = MockRepository.GenerateStrictMock<IPouringGeneratorFactory>();
			var pouringGenerator = MockRepository.GenerateStrictMock<IPouringGenerator>();

			var solver = new Solver(
				pouringGeneratorFactory
			);

			pouringGeneratorFactory
				.Expect(f => f.Create(capacities.Length))
				.Return(pouringGenerator);

			var solutionPouring = new Pouring(capacities, new[] {0, value, 0}, null, null);

			pouringGenerator
				.Stub(g => g.GetNextGeneration(null))
				.IgnoreArguments()
				.Return(new[]
				{
					solutionPouring,
				});

			var solutions = solver
				.SolutionSequence(value, capacities)
				.ToArray();

			Assert.AreEqual(1, solutions.Length);
			Assert.AreEqual(solutionPouring, solutions[0].States().Last());
		}

		[Test]
		public void Test_no_solution()
		{
			var value = 4;
			var capacities = new[] { 3, 5 };

			var pouringGeneratorFactory = MockRepository.GenerateStrictMock<IPouringGeneratorFactory>();
			var pouringGenerator = MockRepository.GenerateStrictMock<IPouringGenerator>();

			var solver = new Solver(
				pouringGeneratorFactory
			);

			pouringGeneratorFactory
				.Expect(f => f.Create(capacities.Length))
				.Return(pouringGenerator);
			
			pouringGenerator
				.Stub(g => g.GetNextGeneration(null))
				.IgnoreArguments()
				.Return(Enumerable.Empty<Pouring>());

			var solutions = solver
				.SolutionSequence(value, capacities)
				.ToArray();

			Assert.AreEqual(0, solutions.Length);
		}

		[Test]
		public void Test_all_capacities_are_less_than_the_value()
		{
			var value = 6;
			var capacities = new[] { 3, 5 };

			var pouringGeneratorFactory = MockRepository.GenerateStrictMock<IPouringGeneratorFactory>();

			var solver = new Solver(
				pouringGeneratorFactory
			);

			var solutions = solver
				.SolutionSequence(value, capacities)
				.ToArray();

			Assert.AreEqual(0, solutions.Length);
		}

		[Test]
		public void Test_only_one_capacity_set()
		{
			var value = 4;
			var capacities = new[] { 5 };

			var pouringGeneratorFactory = MockRepository.GenerateStrictMock<IPouringGeneratorFactory>();
			var pouringGenerator = MockRepository.GenerateStrictMock<IPouringGenerator>();

			var solver = new Solver(
				pouringGeneratorFactory
			);

			pouringGeneratorFactory
				.Expect(f => f.Create(capacities.Length))
				.Return(pouringGenerator);

			var solutionPouring = new Pouring(capacities, new[] { value }, null, null);

			pouringGenerator
				.Stub(g => g.GetNextGeneration(null))
				.IgnoreArguments()
				.Return(new[]
				{
					solutionPouring,
				});

			var solutions = solver
				.SolutionSequence(value, capacities)
				.ToArray();

			Assert.AreEqual(1, solutions.Length);
			Assert.AreEqual(solutionPouring, solutions[0].States().Last());
		}

		[Test]
		public void Test_no_capacity_set()
		{
			var value = 4;
			var capacities = new int[0];

			var pouringGeneratorFactory = MockRepository.GenerateStrictMock<IPouringGeneratorFactory>();
			var pouringGenerator = MockRepository.GenerateStrictMock<IPouringGenerator>();

			var solver = new Solver(
				pouringGeneratorFactory
			);

			pouringGeneratorFactory
				.Expect(f => f.Create(capacities.Length))
				.Return(pouringGenerator);

			var solutions = solver
				.SolutionSequence(value, capacities)
				.ToArray();

			Assert.AreEqual(0, solutions.Length);
		}
	}
}