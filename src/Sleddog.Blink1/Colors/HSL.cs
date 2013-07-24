using System;
using System.Drawing;

namespace Sleddog.Blink1.Colors
{
	internal class HSL
	{
		public ushort Hue { get; private set; }
		public float Saturation { get; private set; }
		public float Luminance { get; private set; }

		public HSL(ushort hue, float saturation, float luminance)
		{
			if (hue > 360)
				throw new ArgumentOutOfRangeException("hue");

			if (saturation < 0 || saturation > 1)
				throw new ArgumentOutOfRangeException("saturation");

			if (luminance < 0 || luminance > 1)
				throw new ArgumentOutOfRangeException("luminance");

			Hue = hue;
			Saturation = saturation;
			Luminance = luminance;
		}

		public static implicit operator Color(HSL hsl)
		{
			var hue = hsl.Hue;
			var saturation = hsl.Saturation;
			var luminance = hsl.Luminance;

			if (luminance.Equals(0))
				return Color.FromArgb(0, 0, 0);

			if (luminance.Equals(100))
				return Color.FromArgb(255, 255, 255);

			var chroma = (1 - Math.Abs(2*luminance - 1))*saturation;
			var hSection = hue/60f;
			var x = chroma*(1 - Math.Abs(hSection%2 - 1));

			var rgbValues = Tuple.Create<float, float, float>(0, 0, 0);

			if (hSection <= 0 && hSection < 1)
				rgbValues = new Tuple<float, float, float>(chroma, x, 0);
			else if (hSection <= 1 && hSection < 2)
				rgbValues = new Tuple<float, float, float>(x, chroma, 0);
			else if (hSection <= 2 && hSection < 3)
				rgbValues = new Tuple<float, float, float>(0, chroma, x);
			else if (hSection <= 3 && hSection < 4)
				rgbValues = new Tuple<float, float, float>(0, x, chroma);
			else if (hSection <= 4 && hSection < 5)
				rgbValues = new Tuple<float, float, float>(x, 0, chroma);
			else if (hSection <= 5 && hSection < 6)
				rgbValues = new Tuple<float, float, float>(chroma, 0, x);

			var m = luminance - 0.5f*chroma;

			rgbValues = Tuple.Create(rgbValues.Item1 + m, rgbValues.Item2 + m, rgbValues.Item3 + m);

			var r = Convert.ToInt32(rgbValues.Item1*255);
			var g = Convert.ToInt32(rgbValues.Item2*255);
			var b = Convert.ToInt32(rgbValues.Item3*255);

			return Color.FromArgb(r, g, b);
		}
	}
}