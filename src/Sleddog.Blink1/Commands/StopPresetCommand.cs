using System;
using Sleddog.Blink1.Interfaces;

namespace Sleddog.Blink1.Commands
{
    internal class StopPresetCommand : IBlink1Command
    {
        public byte[] ToHidCommand()
        {
            return new[]
            {
                Convert.ToByte(1),
                (byte) Blink1Commands.PresetControl,
				       Convert.ToByte(false)
            };
        }
    }
}