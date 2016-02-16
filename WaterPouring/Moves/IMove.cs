namespace WaterPouring.Moves
{
	public interface IMove
	{
		bool CanPerform(Pouring pouring);
		Pouring Perform(Pouring pouring);
	}
}