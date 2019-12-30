using System.Collections.Generic;
using System.Drawing;

namespace Sleddog.Blink1.Tests
{
	public class GoodEnoughColorComparer : IEqualityComparer<Color>
	{
		public bool Equals(Color x, Color y)
		{
			return InRange(x.A, y.A - 1, y.A + 1) &&
			       InRange(x.R, y.R - 1, y.R + 1) &&
			       InRange(x.G, y.G - 1, y.G + 1) &&
			       InRange(x.B, y.B - 1, y.B + 1);
		}

		public int GetHashCode(Color color)
		{
			unchecked
			{
				return (color.A.GetHashCode() * 397) ^ color.R.GetHashCode() ^ color.G.GetHashCode() ^ color.B.GetHashCode();
			}
		}

		private bool InRange(int value, int low, int high)
		{
			return low <= value && value <= high;
		}
	}
}