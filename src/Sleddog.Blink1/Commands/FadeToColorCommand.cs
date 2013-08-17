using System;
using System.Drawing;

namespace Sleddog.Blink1.Commands
{
	internal class FadeToColorCommand : IBlink1Command
	{
		private readonly Color color;
		private readonly Blink1Duration duration;

		public FadeToColorCommand(Color color, Blink1Duration duration)
		{
			this.color = color;
			this.duration = duration;
		}

		public byte[] ToHidCommand()
		{
			return new[] {Convert.ToByte(1), (byte) Blink1Commands.FadeToColor, color.R, color.G, color.B, duration.High, duration.Low};
		}
	}
}