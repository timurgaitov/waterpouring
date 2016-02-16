using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Mvc;
using WaterPouring;
using WaterPouringService.DTO;
using WaterPouringService.Inject;

namespace WaterPouringService.Controllers
{
	[Route("api/v1/waterpouring")]
	public class WaterPouringController : Controller
	{
		private const int maxSolutions = 2;
		
		private static readonly ISolver solver = Container.Get<ISolver>();

		[HttpGet("solve")]
		public IList<DTO.Solution> Get(int value, int[] capacities)
		{
			return solver
				.SolutionSequence(value, capacities)
				.Take(maxSolutions)
				.Select(solution => new DTO.Solution
				{
					Steps = solution
						.States()
						.Select(state => new SolutionStep
						{
							Capacities = state.Capacities,
							State = state.State,
							Move = state.Move?.ToString(),
						})
						.ToArray()
				})
				.ToArray(); ;
		}
	}
}
