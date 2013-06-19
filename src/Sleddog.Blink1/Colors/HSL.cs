using System;
using System.Drawing;

namespace Sleddog.Blink1.Colors
{
	internal class HSL
	{
		public ushort Hue { get; private set; }
		public ushort Saturation { get; private set; }
		public ushort Luminance { get; private set; }

		public HSL(ushort hue, ushort saturation, ushort luminance)
		{
			if (hue > 360)
				throw new ArgumentOutOfRangeException("hue");

			if (saturation > 100)
				throw new ArgumentOutOfRangeException("saturation");

			if (luminance > 100)
				throw new ArgumentOutOfRangeException("luminance");

			Hue = hue;
			Saturation = saturation;
			Luminance = luminance;
		}

		public static implicit operator Color(HSL hsl)
		{
			if (hsl.Saturation == 0)
			{
				var colorValue = (float) hsl.Luminance/100*255;

				var intColorValue = Convert.ToInt32(colorValue);

				return Color.FromArgb(intColorValue, intColorValue, intColorValue);
			}
			else
			{
			}

			return Color.Red;
		}
	}
}