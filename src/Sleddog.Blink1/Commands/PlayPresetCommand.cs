using System;
using Sleddog.Blink1.Interfaces;

namespace Sleddog.Blink1.Commands
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
                Convert.ToByte(1),
                (byte) Blink1Commands.PresetControl,
                Convert.ToByte(true),
                startPosition
                       endPosition,
                       count,
                       Convert.ToByte(0),
                       Convert.ToByte(0)
            };
        }
    }
}