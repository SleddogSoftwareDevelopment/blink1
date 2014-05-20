using System;
using System.Drawing;

namespace Sleddog.Blink1
{
    public interface IBlink1
    {
        Version Version { get; }
        string SerialNumber { get; }
        bool Blink(Color color, TimeSpan interval, ushort times);
        bool Set(Color color);
        bool Fade(Color color, TimeSpan fadeDuration);
        bool Show(Color color, TimeSpan visibleTime);
        bool Save(Blink1Preset preset, ushort position);
        Blink1Preset ReadPreset(ushort position);
        bool Play(ushort startPosition);
        bool Pause();
        bool EnableInactivityMode(TimeSpan waitDuration);
        bool DisableInactivityMode();
        void TurnOff();
        void Dispose();
    }
}