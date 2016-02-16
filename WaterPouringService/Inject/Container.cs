using System;
using Ninject;

namespace WaterPouringService.Inject
{
	public static class Container
	{
		private static readonly Lazy<IKernel> kernel = new Lazy<IKernel>(
			() => new StandardKernel(new ContainerModule())
		);

		public static void Override<TItf, T>()
			where T : TItf
		{
			kernel.Value.Rebind<TItf>().To<T>();
		}

		public static T Get<T>()
		{
			return kernel.Value.Get<T>();
		}
	}
}