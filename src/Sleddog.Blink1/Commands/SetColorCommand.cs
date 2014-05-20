using System;
using System.Drawing;
using Sleddog.Blink1.Interfaces;
using Sleddog.Blink1.Internal;

namespace Sleddog.Blink1.Commands
{
    internal class SetColorCommand : IBlink1Command
    {
        private readonly Color color;

        public SetColorCommand(Color color)
        {
            this.color = color;
        }

        public byte[] ToHidCommand()
        {
            return new[]
            {
                Convert.ToByte(1),
                (byte) Blink1Commands.SetColor,
                color.R,
                color.G,
                color.B
            };
        }
    }
}