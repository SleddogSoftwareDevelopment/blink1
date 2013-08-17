using System;

namespace Sleddog.Blink1
{
	public static class TimeSpanExt
	{
		public static Blink1Duration ToBlink1Duration(this TimeSpan timeSpan)
		{
			var timeInMilliSeconds = Convert.ToInt32(timeSpan.TotalMilliseconds);

			var result = new Blink1Duration
			             {
				             High = Convert.ToByte((timeInMilliSeconds/10) >> 8),
				             Low = Convert.ToByte((timeInMilliSeconds/10) & 0xFF)
			             };

			return result;
		}
	}
}