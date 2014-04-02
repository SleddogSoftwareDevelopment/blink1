using System;
using System.Drawing;

namespace Sleddog.Blink1
{
    public class Blink1Mk2 : Blink1, IBlink1Mk2, IDisposable
    {
        internal Blink1Mk2(Blink1CommandBus commandBus) : base(commandBus)
        {
        }

        public bool SetColor(Color color, LEDPosition ledPosition)
        {
            throw new NotImplementedException();
        }

        public bool PlaybackPresets(ushort startPosition, ushort endPosition, ushort count)
        {
            throw new NotImplementedException();
        }

        public PlaybackStatus ReadPlaybackStatus()
        {
            throw new NotImplementedException();
        }
    }
}