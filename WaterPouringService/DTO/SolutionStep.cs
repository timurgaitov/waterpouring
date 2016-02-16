using System.Collections.Generic;

namespace WaterPouringService.DTO
{
	public class SolutionStep
	{
		public IReadOnlyList<int> Capacities { get; set; }
		public IReadOnlyList<int> State { get; set; }
		public string Move { get; set; }
	}
}