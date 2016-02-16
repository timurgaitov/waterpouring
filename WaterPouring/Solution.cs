using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WaterPouring
{
	public class Solution
	{
		private readonly Pouring last;

		public Solution(Pouring last)
		{
			this.last = last;
		}

		public IEnumerable<Pouring> States()
		{
			return LastToFirst()
				.Reverse();
		}

		public override string ToString()
		{
			var sb = new StringBuilder();

			foreach (var pouring in States())
			{
				sb.AppendLine(pouring.Move?.ToString());
				sb.AppendLine(pouring.ToString());
			}

			return sb.ToString();
		}

		private IEnumerable<Pouring> LastToFirst()
		{
			var current = last;

			while (current != null)
			{
				yield return current;
				current = current.BeforeMove;
			}
		} 
	}
}