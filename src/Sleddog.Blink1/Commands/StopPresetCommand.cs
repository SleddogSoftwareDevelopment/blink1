using System;
using Sleddog.Blink1.Internal;
using Sleddog.Blink1.Internal.Interfaces;

namespace Sleddog.Blink1.Commands
{
    internal class StopPresetCommand : IBlink1Command
    {
        public byte[] ToHidCommand()
        {
            return new[]
            {
                (byte) Blink1Commands.PresetControl,
                Convert.ToByte(false)
            };
        }
    }
}