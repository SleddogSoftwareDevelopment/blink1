using System;
using System.Drawing;

namespace Sleddog.Blink1
{
	public interface IBlink1Mk2 : IBlink1
	{
		bool Fade(Color color, TimeSpan fadeDuration, LEDPosition ledPosition);
		bool Play(ushort startPosition, ushort endPosition, ushort count);
		PlaybackStatus ReadPlaybackStatus();
		bool EnabledInactivityMode(TimeSpan waitDuration, bool maintainState, ushort startPosition, ushort endPosition);
		bool SavePresets();
	}
}