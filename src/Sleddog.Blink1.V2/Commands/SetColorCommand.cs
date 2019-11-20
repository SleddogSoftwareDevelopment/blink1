using System.Drawing;
using Sleddog.Blink1.V2.Internal;
using Sleddog.Blink1.V2.Internal.Interfaces;

namespace Sleddog.Blink1.V2.Commands
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
                (byte) Blink1Commands.SetColor,
                color.R,
                color.G,
                color.B
            };
        }
    }
}