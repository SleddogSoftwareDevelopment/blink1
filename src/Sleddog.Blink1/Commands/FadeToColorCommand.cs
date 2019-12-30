using System;
using System.Drawing;
using Sleddog.Blink1.Internal;

namespace Sleddog.Blink1.Commands
{
	internal class FadeToColorCommand : Blink1Command
	{
		private readonly Color color;
		private readonly Blink1Duration duration;
		private readonly LEDPosition ledPosition;

		public FadeToColorCommand(Color color, Blink1Duration duration) : this(color, duration, LEDPosition.Both)
		{ }

		public FadeToColorCommand(Color color, Blink1Duration duration, LEDPosition ledPosition)
		{
			this.color = color;
			this.duration = duration;
			this.ledPosition = ledPosition;
		}

		protected override byte[] HidCommandData()
		{
			return new[]
			{
				(byte) Blink1Commands.FadeToColor,
				color.R,
				color.G,
				color.B,
				duration.High,
				duration.Low,
				Convert.ToByte(ledPosition)
			};
		}
	}
}