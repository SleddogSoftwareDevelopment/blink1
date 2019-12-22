using System;
using System.Drawing;
using Sleddog.Blink1.Commands;
using Sleddog.Blink1.Internal;

namespace Sleddog.Blink1
{
	public class Blink1Mk2 : Blink1, IBlink1Mk2
	{
		internal Blink1Mk2(Blink1CommandBus commandBus)
			: base(commandBus, 32)
		{ }

		public new bool EnableGamma
		{
			get => base.EnableGamma;
			set { }
		}

		public bool Fade(Color color, TimeSpan fadeDuration, LEDPosition ledPosition)
		{
			var command = new FadeToColorCommand(color, fadeDuration, ledPosition);

			return CommandBus.SendCommand(command);
		}

		public bool Play(ushort startPosition, ushort endPosition, ushort count)
		{
			var command = new PlayPresetCommand(startPosition, endPosition, count);

			return CommandBus.SendCommand(command);
		}

		public bool SavePresets()
		{
			var command = new SavePresetsCommand();

			return CommandBus.SendCommand(command);
		}

		public bool EnabledInactivityMode(TimeSpan waitDuration, bool maintainState, ushort startPosition,
			ushort endPosition)
		{
			var command = new EnableInactivityModeCommand(waitDuration, maintainState, startPosition, endPosition);

			return CommandBus.SendCommand(command);
		}

		public PlaybackStatus ReadPlaybackStatus()
		{
			var query = new ReadPlaybackStateQuery();

			return CommandBus.SendQuery(query);
		}
	}
}