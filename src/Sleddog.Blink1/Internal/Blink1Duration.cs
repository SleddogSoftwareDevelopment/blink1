using System;

namespace Sleddog.Blink1.Internal
{
    internal class Blink1Duration
    {
        public byte High { get; }
        public byte Low { get; }

        public Blink1Duration(TimeSpan duration)
        {
            var blinkTime = Convert.ToUInt32(duration.TotalMilliseconds/10);

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
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != GetType())
            {
                return false;
            }

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

            var blinkTime = Convert.ToUInt32((high << 8) | low);

            return TimeSpan.FromMilliseconds(blinkTime*10);
        }

        public static implicit operator Blink1Duration(TimeSpan duration)
        {
            return new Blink1Duration(duration);
        }
    }
}