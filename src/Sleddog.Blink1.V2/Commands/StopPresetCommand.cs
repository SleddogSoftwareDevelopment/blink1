using System;
using Sleddog.Blink1.V2.Internal;
using Sleddog.Blink1.V2.Internal.Interfaces;

namespace Sleddog.Blink1.V2.Commands
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