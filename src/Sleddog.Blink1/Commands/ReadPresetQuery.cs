using System;
using System.Drawing;
using Sleddog.Blink1.Internal;
using Sleddog.Blink1.Internal.Interfaces;

namespace Sleddog.Blink1.Commands
{
    internal class ReadPresetQuery : IBlink1Query<Blink1Preset>
    {
        private readonly byte position;

        public ReadPresetQuery(ushort position)
        {
            this.position = Convert.ToByte(position);
        }

        public Blink1Preset ToResponseType(byte[] responseData)
        {
            var color = Color.FromArgb(responseData[2], responseData[3], responseData[4]);
            var duration = new Blink1Duration(responseData[5], responseData[6]);

            return new Blink1Preset(color, duration);
        }

        public byte[] ToHidCommand()
        {
            return new[]
            {
                (byte) Blink1Commands.ReadPreset,
                Convert.ToByte(0),
                Convert.ToByte(0),
                Convert.ToByte(0),
                Convert.ToByte(0),
                Convert.ToByte(0),
                position
            };
        }
    }
}