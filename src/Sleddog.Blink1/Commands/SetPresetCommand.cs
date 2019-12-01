using System;
using Sleddog.Blink1.Internal;
using Sleddog.Blink1.Internal.Interfaces;

namespace Sleddog.Blink1.Commands
{
    internal class SetPresetCommand : IBlink1Command
    {
        private readonly Blink1Preset preset;
        private readonly byte position;

        public SetPresetCommand(Blink1Preset preset, ushort position)
        {
            this.preset = preset;
            this.position = Convert.ToByte(position);
        }

        public byte[] ToHidCommand()
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