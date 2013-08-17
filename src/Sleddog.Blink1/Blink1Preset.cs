using System;
using System.Drawing;

namespace Sleddog.Blink1
{
	public class Blink1Preset
	{
		public Blink1Preset(Color color, TimeSpan duration)
		{
			Color = color;
			Duration = new Blink1Duration(duration);
		}

		public Color Color { get; private set; }
		public Blink1Duration Duration { get; private set; }

		protected bool Equals(Blink1Preset other)
		{
			var equal = Color.R.Equals(other.Color.R) &&
			            Color.G.Equals(other.Color.G) &&
			            Color.B.Equals(other.Color.B) &&
			            Duration.Equals(other.Duration);

			return equal;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj))
				return false;
			if (ReferenceEquals(this, obj))
				return true;
			if (obj.GetType() != GetType())
				return false;
			return Equals((Blink1Preset) obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return (Color.GetHashCode()*397) ^ (Duration != null ? Duration.GetHashCode() : 0);
			}
		}

		public static bool operator ==(Blink1Preset left, Blink1Preset right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(Blink1Preset left, Blink1Preset right)
		{
			return !Equals(left, right);
		}
	}
}