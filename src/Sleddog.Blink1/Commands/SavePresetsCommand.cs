using System;
using Sleddog.Blink1.Internal;
using Sleddog.Blink1.Internal.Interfaces;

namespace Sleddog.Blink1.Commands
{
	internal class SavePresetsCommand : IBlink1Command
	{
		public byte[] ToHidCommand()
		{
			return new[]
			{
				(byte) Blink1Commands.SavePresetMk2,
				Convert.ToByte(0xBE),
				Convert.ToByte(0xEF),
				Convert.ToByte(0xCA),
				Convert.ToByte(0xFE),
				Convert.ToByte(0x00),
				Convert.ToByte(0x00)
			};
		}
	}
}