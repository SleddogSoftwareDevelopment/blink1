using System;
using System.Linq;

namespace Sleddog.Blink1.Commands
{
	public class PresetControlCommand: IBlink1Command
	{
		private readonly bool enabled;
		private readonly byte startPosition;

		public PresetControlCommand(bool enabled, int startPosition = 0) {
			if (!Enumerable.Range(0, Blink1.NumberOfPresets).Contains(startPosition)) {
				throw new ArgumentOutOfRangeException("startPosition");
			}

			this.enabled = enabled;
			this.startPosition = Convert.ToByte(startPosition);
		}

		public byte[] ToHidCommand() {
			return new[] {
				Convert.ToByte(1), 
				(byte) Blink1Commands.PresetControl, 
				Convert.ToByte(enabled), 
				startPosition
			};
		}
	}
}
