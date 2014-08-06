using System;
using Sleddog.Blink1.Interfaces;

namespace Sleddog.Blink1.Commands
{
    internal class EnableInactivityModeCommand : IBlink1Command
    {
        private readonly Blink1Duration waitDuration;

        public EnableInactivityModeCommand(Blink1Duration waitDuration)
        {
            this.waitDuration = waitDuration;
        }

        public byte[] ToHidCommand()
        {
            return new[]
            {
                Convert.ToByte(1),
                (byte) Blink1Commands.InactivityMode,
                Convert.ToByte(true),
                waitDuration.High,
                waitDuration.Low
            };
        }
    }
}