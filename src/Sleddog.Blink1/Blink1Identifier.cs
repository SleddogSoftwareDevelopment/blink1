using System.Drawing;

namespace Sleddog.Blink1
{
    public class Blink1Identifier
    {
        public IBlink1 Blink1 { get; set; }
        public Color Color { get; set; }

        public Blink1Identifier(IBlink1 blink1, Color color)
        {
            Blink1 = blink1;
            Color = color;
        }
    }
}