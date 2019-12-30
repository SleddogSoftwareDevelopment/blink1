using System;
using Sleddog.Blink1.Internal;

namespace Sleddog.Blink1.Commands
{
	internal class SetPresetCommand : Blink1Command
	{
		private readonly byte position;
		private readonly Blink1Preset preset;

		public SetPresetCommand(Blink1Preset preset, ushort position)
		{
			this.preset = preset;
			this.position = Convert.ToByte(position);
		}

		protected override byte[] HidCommandData()
		{
			var presetDuration = preset.PresetDuration;
			var presetColor = preset.Color;

			return new[]
			{
				(byte) Blink1Commands.SavePreset,
				presetColor.R,
				presetColor.G,
				presetColor.B,
				presetDuration.High,
				presetDuration.Low,
				position
			};
		}
	}
}