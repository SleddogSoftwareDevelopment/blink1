using System;
using Sleddog.Blink1.V2.Internal;
using Sleddog.Blink1.V2.Internal.Interfaces;

namespace Sleddog.Blink1.V2.Commands
{
    internal class PlayPresetCommand : IBlink1Command
    {
        private readonly byte startPosition;
        private readonly byte endPosition;
        private readonly byte count;

        public PlayPresetCommand(ushort startPosition) : this(startPosition, 0, 0)
        {
        }

        public PlayPresetCommand(ushort startPosition, ushort endPosition, ushort count)
        {
            this.startPosition = Convert.ToByte(startPosition);
            this.endPosition = Convert.ToByte(endPosition);
            this.count = Convert.ToByte(count);
        }

        public byte[] ToHidCommand()
        {
            return new[]
            {
                (byte) Blink1Commands.PresetControl,
                Convert.ToByte(true),
                startPosition,
                endPosition,
                count
            };
        }
    }
}