using System;
using System.Drawing;

namespace Sleddog.Blink1.Colors
{
	internal class GammaCorrector
	{
		private static readonly double GammaValue = 2.2;
		private static readonly double InverseGammaValue = 1 / GammaValue;

		public Color Encode(Color color)
		{
			var r = color.R;
			var g = color.G;
			var b = color.B;

			return Color.FromArgb(Encode(r), Encode(g), Encode(b));
		}

		public Color Decode(Color color)
		{
			var r = color.R;
			var g = color.G;
			var b = color.B;

			return Color.FromArgb(Decode(r), Decode(g), Decode(b));
		}

		private int Encode(int value)
		{
			var correctedValue = Math.Pow(value / (double) 255, GammaValue) * 255;

			return (int) Math.Round(correctedValue);
		}

		private int Decode(int value)
		{
			var correctedValue = Math.Pow(value / (double) 255, InverseGammaValue) * 255;

			return (int) Math.Round(correctedValue);
		}
	}
}