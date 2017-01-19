using System.Drawing;

namespace Sleddog.Blink1
{
    public class Blink1Identifier
    {
        public IBlink1 Blink1 { get; }
        public Color Color { get; }

        public Blink1Identifier(IBlink1 blink1, Color color)
        {
            Blink1 = blink1;
            Color = color;
        }
    }
}