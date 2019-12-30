using System;
using Sleddog.Blink1.Internal;

namespace Sleddog.Blink1.Commands
{
	internal class SavePresetsCommand : Blink1Command
	{
		protected override byte[] HidCommandData()
		{
			return new[]
			{
				(byte) Blink1Commands.SavePresetMk2,
				Convert.ToByte(0xBE),
				Convert.ToByte(0xEF),
				Convert.ToByte(0xCA),
				Convert.ToByte(0xFE)
			};
		}
	}
}