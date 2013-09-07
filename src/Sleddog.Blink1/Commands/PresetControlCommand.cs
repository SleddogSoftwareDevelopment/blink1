using System;

namespace Sleddog.Blink1.Commands
{
	public class PresetControlCommand : IBlink1Command
	{
		private readonly bool enabled;
		private readonly byte startPosition;

		public PresetControlCommand(bool enabled) : this(enabled, 0)
		{
		}

		public PresetControlCommand(bool enabled, ushort startPosition)
		{
			this.enabled = enabled;
			this.startPosition = Convert.ToByte(startPosition);
		}

		public byte[] ToHidCommand()
		{
			return new[]
			       {
				       Convert.ToByte(1),
				       (byte) Blink1Commands.PresetControl,
				       Convert.ToByte(enabled),
				       startPosition
			       };
		}
	}
}