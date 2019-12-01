namespace Sleddog.Blink1.Colors
{
    internal class GammaCorrector
    {
        private static readonly double GammaValue = 2.2;

        public Color Encode(Color color)
        {
            var r = color.R;
            var g = color.G;
            var b = color.B;

            return Color.FromArgb(Encode(r), Encode(g), Encode(b));
        }

        private int Encode(int value)
        {
            var correctedValue = Math.Pow((value/(double) 255), GammaValue)*255;

            return (int) Math.Round(correctedValue);
        }
    }
}