using Ninject.Modules;
using WaterPouring;

namespace WaterPouringService.Inject
{
	internal class ContainerModule : NinjectModule
	{
		public override void Load()
		{
			Bind<ISolver>()
				.To<Solver>().InSingletonScope();

			Bind<IPouringGeneratorFactory>()
				.To<PouringGeneratorFactory>().InSingletonScope();
		}
	}
}