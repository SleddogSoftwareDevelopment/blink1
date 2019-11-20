using System;
using Sleddog.Blink1.V2.Internal;
using Sleddog.Blink1.V2.Internal.Interfaces;

namespace Sleddog.Blink1.V2.Commands
{
    internal class DisableInactivityModeCommand : IBlink1Command
    {
        public byte[] ToHidCommand()
        {
            return new[]
            {
                (byte) Blink1Commands.InactivityMode,
                Convert.ToByte(false)
            };
        }
    }
}