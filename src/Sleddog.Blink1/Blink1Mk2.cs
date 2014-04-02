using System;
using System.Drawing;

namespace Sleddog.Blink1
{
    public class Blink1Mk2:IBlink1Mk2,IDisposable
    {
        public Version Version { get; private set; }
        public string SerialNumber { get; private set; }

        public bool Blink(Color color, TimeSpan interval, ushort times)
        {
            throw new NotImplementedException();
        }

        public bool SetColor(Color color)
        {
            throw new NotImplementedException();
        }

        public bool FadeToColor(Color color, TimeSpan fadeDuration)
        {
            throw new NotImplementedException();
        }

        public bool ShowColor(Color color, TimeSpan visibleTime)
        {
            throw new NotImplementedException();
        }

        public bool SavePreset(Blink1Preset preset, ushort position)
        {
            throw new NotImplementedException();
        }

        public Blink1Preset ReadPreset(ushort position)
        {
            throw new NotImplementedException();
        }

        public bool PlaybackPresets(ushort startPosition)
        {
            throw new NotImplementedException();
        }

        public bool PausePresets()
        {
            throw new NotImplementedException();
        }

        public bool EnableInactivityMode(TimeSpan waitDuration)
        {
            throw new NotImplementedException();
        }

        public bool DisableInactivityMode()
        {
            throw new NotImplementedException();
        }

        public void TurnOff()
        {
            throw new NotImplementedException();
        }

        void IBlink1.Dispose()
        {
            throw new NotImplementedException();
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

        void IDisposable.Dispose()
        {
            throw new NotImplementedException();
        }
    }
}