using System;

namespace Sleddog.Blink1.Commands
{
	public class SetPresetCommand: IBlink1Command
	{
		private readonly Blink1Preset preset;
		private readonly byte position;

		public SetPresetCommand(Blink1Preset preset, int position) {
			this.preset = preset;
			this.position = Convert.ToByte(position);
		}

		public byte[] ToHidCommand() {
			//- Set color pattern line  format: {0x01, 'P', r,g,b,     th,tl, p }
			Blink1Duration duration = preset.Duration.ToBlink1Duration();

			return new[] {
				Convert.ToByte(1),
				(byte) Blink1Commands.SavePreset,
				preset.Color.R,
				preset.Color.G,
				preset.Color.B,
				duration.High,
				duration.Low,
				position
			};
		}
	}
}
