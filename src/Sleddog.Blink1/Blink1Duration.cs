using System;

namespace Sleddog.Blink1
{
	internal class Blink1Duration
	{
		private const int Blink1UpdateFrequency = 10;

		public byte High { get; private set; }
		public byte Low { get; private set; }

		public Blink1Duration(TimeSpan duration)
		{
			var durationInMilliseconds = Convert.ToInt32(duration.TotalMilliseconds);

			var blinkTime = (durationInMilliseconds/Blink1UpdateFrequency);

			High = Convert.ToByte(blinkTime >> 8);
			Low = Convert.ToByte(blinkTime & 0xFF);
		}

	    internal Blink1Duration(byte high, byte low)
	    {
	        High = high;
	        Low = low;
	    }

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
			if (obj.GetType() != GetType())
				return false;
			return Equals((Blink1Duration) obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return (High.GetHashCode()*397) ^ Low.GetHashCode();
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

		public static implicit operator TimeSpan(Blink1Duration duration)
		{
			var low = duration.Low;
			var high = duration.High;

			var blinkTime = Convert.ToInt32(high | low << 8);

			return TimeSpan.FromMilliseconds(blinkTime*Blink1UpdateFrequency);
		}

	    public static implicit operator Blink1Duration(TimeSpan duration)
	    {
	        return new Blink1Duration(duration);
	    }
	}
}