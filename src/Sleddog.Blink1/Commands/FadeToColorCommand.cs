using System;
using System.Drawing;

namespace Sleddog.Blink1.Commands
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

		public byte[] ToHidCommand()
		{
			var duration = fadeTime.ToBlink1Duration();

			return new[] {Convert.ToByte(1), (byte) Blink1Commands.FadeToColor, color.R, color.G, color.B, duration.High, duration.Low};
		}
	}
}