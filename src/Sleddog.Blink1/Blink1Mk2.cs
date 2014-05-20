using System;
using Sleddog.Blink1.Commands;
using Sleddog.Blink1.Internal;

namespace Sleddog.Blink1
{
    public class Blink1Mk2 : Blink1, IBlink1Mk2, IDisposable
    {
        private const ushort NumberOfPresets = 32;

        internal Blink1Mk2(Blink1CommandBus commandBus)
            : base(commandBus)
        {
        }

        public bool EnabledInactivityMode(TimeSpan waitDuration, bool maintainState, ushort startPosition,
            ushort endPosition)
        {
            var command = new EnableInactivityModeCommand(waitDuration, maintainState, startPosition, endPosition);

            return commandBus.SendCommand(command);
        }

        public bool Play(ushort startPosition, ushort endPosition, ushort count)
        {
            var command = new PlayPresetCommand(startPosition, endPosition, count);

            return commandBus.SendCommand(command);
        }

        public bool SavePresets()
        {
            var command = new SavePresetsCommand();

            return commandBus.SendCommand(command);
        }

        public PlaybackStatus ReadPlaybackStatus()
        {
            throw new NotImplementedException();
        }
    }
}