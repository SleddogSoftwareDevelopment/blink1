using System;
using Sleddog.Blink1.Internal;
using Sleddog.Blink1.Internal.Interfaces;

namespace Sleddog.Blink1.Commands
{
    internal class DisableInactivityModeCommand : IBlink1Command
    {
        public byte[] ToHidCommand()
        {
            return new[]
            {
                Convert.ToByte(1),
                (byte) Blink1Commands.InactivityMode,
                Convert.ToByte(false)
            };
        }
    }
}