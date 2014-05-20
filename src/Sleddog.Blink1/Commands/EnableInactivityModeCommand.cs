using System;
using Sleddog.Blink1.Internal;
using Sleddog.Blink1.Internal.Interfaces;

namespace Sleddog.Blink1.Commands
{
    internal class EnableInactivityModeCommand : IBlink1Command
    {
        private readonly Blink1Duration waitDuration;
        private readonly bool maintainState;
        private readonly ushort startPosition;
        private readonly ushort endPosition;

        public EnableInactivityModeCommand(Blink1Duration waitDuration)
        {
            this.waitDuration = waitDuration;
        }

        public EnableInactivityModeCommand(Blink1Duration waitDuration, bool maintainState, ushort startPosition,
            ushort endPosition)
        {
            this.waitDuration = waitDuration;
            this.maintainState = maintainState;
            this.startPosition = startPosition;
            this.endPosition = endPosition;
        }

        public byte[] ToHidCommand()
        {
            return new[]
            {
                Convert.ToByte(1),
                (byte) Blink1Commands.InactivityMode,
                Convert.ToByte(true),
                waitDuration.High,
                waitDuration.Low,
                Convert.ToByte(maintainState),
                Convert.ToByte(startPosition),
                Convert.ToByte(endPosition)
            };
        }
    }
}