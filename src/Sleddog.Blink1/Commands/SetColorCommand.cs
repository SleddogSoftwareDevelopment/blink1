using System.Drawing;
using Sleddog.Blink1.Internal;

namespace Sleddog.Blink1.Commands
{
	internal class SetColorCommand : Blink1Command
	{
		private readonly Color color;

		public SetColorCommand(Color color)
		{
			this.color = color;
		}

		protected override byte[] HidCommandData()
		{
			return new[]
			{
				(byte) Blink1Commands.SetColor,
				color.R,
				color.G,
				color.B
			};
		}
	}
}