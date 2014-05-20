using System;

namespace Sleddog.Blink1
{
    public interface IBlink1Mk2 : IBlink1
    {
        bool Play(ushort startPosition, ushort endPosition, ushort count);
        PlaybackStatus ReadPlaybackStatus();
        bool EnabledInactivityMode(TimeSpan waitDuration, bool maintainState, ushort startPosition, ushort endPosition);
        bool SavePresets();
    }
}