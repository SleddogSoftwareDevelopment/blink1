using System;
using System.Drawing;

namespace Sleddog.Blink.Commands
{
	internal class FadeToColorCommand : IBlink1Command
	{
		private readonly Color color;
		private readonly TimeSpan fadeTime;

		public FadeToColorCommand(Color color, TimeSpan fadeTime)
		{
			this.color = color;
			this.fadeTime = fadeTime;
		}

		public byte[] ToByteArray()
		{
			var timeInMilliSeconds = Convert.ToInt32(fadeTime.TotalMilliseconds);

			var fadeTimePart0 = Convert.ToByte((timeInMilliSeconds/10) >> 8);
			var fadeTimePart1 = Convert.ToByte((timeInMilliSeconds/10) & 0xFF);

			return new[] {Convert.ToByte(1), (byte) Blink1Commands.FadeToColor, color.R, color.G, color.B, fadeTimePart0, fadeTimePart1};
		}
	}
}