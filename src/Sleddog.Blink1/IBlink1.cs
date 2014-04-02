using System;
using System.Drawing;

namespace Sleddog.Blink1
{
    public interface IBlink1
    {
        Version Version { get; }
        string SerialNumber { get; }
        bool Blink(Color color, TimeSpan interval, ushort times);
        bool SetColor(Color color);
        bool FadeToColor(Color color, TimeSpan fadeDuration);
        bool ShowColor(Color color, TimeSpan visibleTime);
        bool SavePreset(Blink1Preset preset, ushort position);
        Blink1Preset ReadPreset(ushort position);
        bool PlaybackPresets(ushort startPosition);
        bool PausePresets();
        bool EnableInactivityMode(TimeSpan waitDuration);
        bool DisableInactivityMode();
        void TurnOff();
        void Dispose();
    }
}