using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sleddog.Blink1.Commands
{
	public class PresetControlCommand: IBlink1Command
	{
		private readonly bool enabled;
		private readonly byte startPosition;

		public PresetControlCommand(bool enabled, int startPosition = 0) {
			if (startPosition < 0 || startPosition > 11) {
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
