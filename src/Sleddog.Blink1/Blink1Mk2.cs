using System;
using System.Drawing;
using Sleddog.Blink1.Commands;

namespace Sleddog.Blink1
{
    public class Blink1Mk2 : Blink1, IBlink1Mk2, IDisposable
    {
        private const ushort NumberOfPresets = 32;

        internal Blink1Mk2(Blink1CommandBus commandBus)
            : base(commandBus)
        {
        }

        public bool SetColor(Color color, LEDPosition ledPosition)
        {
            throw new NotImplementedException();
        }

        public bool EnabledInactivityMode(TimeSpan waitDuration, bool maintainState, ushort startPosition,
            ushort endPosition)
        {
            var command = new EnableInactivityModeCommand(waitDuration, maintainState, startPosition, endPosition);

            return commandBus.SendCommand(command);
        }

        public bool PlaybackPresets(ushort startPosition, ushort endPosition, ushort count)
        {
            var command = new PlayPresetCommand(startPosition, endPosition, count);

            return commandBus.SendCommand(command);
        }

        public PlaybackStatus ReadPlaybackStatus()
        {
            throw new NotImplementedException();
        }
    }
}