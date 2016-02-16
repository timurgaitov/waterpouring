namespace WaterPouring
{
	public interface IPouringGeneratorFactory
	{
		IPouringGenerator Create(int numberOfCups);
	}

	public class PouringGeneratorFactory : IPouringGeneratorFactory
	{
		public IPouringGenerator Create(int numberOfCups)
		{
			return new PouringGenerator(numberOfCups);
		}
	}
}