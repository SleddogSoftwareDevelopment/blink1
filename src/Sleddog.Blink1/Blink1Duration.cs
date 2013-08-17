using System;

namespace Sleddog.Blink1
{
	public class Blink1Duration
	{
		public Blink1Duration(TimeSpan duration)
		{
			var durationInMilliseconds = Convert.ToInt32(duration.TotalMilliseconds);

			High = Convert.ToByte((durationInMilliseconds/10) >> 8);
			Low = Convert.ToByte((durationInMilliseconds/10) & 0xFF);
		}

		public byte High { get; private set; }
		public byte Low { get; private set; }
	
		protected bool Equals(Blink1Duration other)
		{
			return High == other.High && Low == other.Low;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj))
				return false;
			if (ReferenceEquals(this, obj))
				return true;
			if (obj.GetType() != this.GetType())
				return false;
			return Equals((Blink1Duration)obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return (High.GetHashCode() * 397) ^ Low.GetHashCode();
			}
		}

		public static bool operator ==(Blink1Duration left, Blink1Duration right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(Blink1Duration left, Blink1Duration right)
		{
			return !Equals(left, right);
		}
	}
}