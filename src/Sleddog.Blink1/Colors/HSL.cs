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

			if (luminance.Equals(1))
				return Color.FromArgb(255, 255, 255);

			var chroma = (1 - Math.Abs(2*luminance - 1))*saturation;
			var hSection = hue/60f;
			var x = chroma*(1 - Math.Abs(hSection%2 - 1));

			var rgbValues = Tuple.Create(0f, 0f, 0f);

			if (hSection >= 0 && hSection < 1)
				rgbValues = Tuple.Create(chroma, x, 0f);
			else if (hSection >= 1 && hSection < 2)
				rgbValues = Tuple.Create(x, chroma, 0f);
			else if (hSection >= 2 && hSection < 3)
				rgbValues = Tuple.Create(0f, chroma, x);
			else if (hSection >= 3 && hSection < 4)
				rgbValues = Tuple.Create(0f, x, chroma);
			else if (hSection >= 4 && hSection < 5)
				rgbValues = Tuple.Create(x, 0f, chroma);
			else if (hSection >= 5 && hSection < 6)
				rgbValues = Tuple.Create(chroma, 0f, x);

			var m = luminance - 0.5f*chroma;

			var modRGBValues = Tuple.Create(rgbValues.Item1 + m, rgbValues.Item2 + m, rgbValues.Item3 + m);

			var r = (int) Math.Floor(modRGBValues.Item1*255);
			var g = (int) Math.Floor(modRGBValues.Item2*255);
			var b = (int) Math.Floor(modRGBValues.Item3*255);

			return Color.FromArgb(r, g, b);
		}
	}
}