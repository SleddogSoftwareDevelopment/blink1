using System;
using Sleddog.Blink1.Internal;

namespace Sleddog.Blink1.Commands
{
	internal class StopPresetCommand : Blink1Command
	{
		protected override byte[] HidCommandData()
		{
			return new[]
			{
				(byte) Blink1Commands.PresetControl,
				Convert.ToByte(false)
			};
		}
	}
}