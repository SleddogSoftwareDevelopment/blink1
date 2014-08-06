using System;
using Sleddog.Blink1.Interfaces;

namespace Sleddog.Blink1.Commands
{
    internal class PlayPresetCommand : IBlink1Command
    {
        private readonly byte startPosition;

        public PlayPresetCommand(ushort startPosition)
        {
            this.startPosition = Convert.ToByte(startPosition);
        }

        public byte[] ToHidCommand()
        {
            return new[]
            {
                Convert.ToByte(1),
                (byte) Blink1Commands.PresetControl,
                Convert.ToByte(true),
                startPosition
            };
        }
    }
}