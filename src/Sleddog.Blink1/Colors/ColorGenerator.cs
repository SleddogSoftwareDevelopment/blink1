using System;
using System.Collections.Generic;
using System.Drawing;

namespace Sleddog.Blink1.Colors
{
	public class ColorGenerator
	{
		private static readonly Random Random = new Random();

		public List<Color> GenerateColors(int colorCount)
		{
			var colors = new List<Color>();

			var cellRange = 1f/colorCount;
			var cellOffset = Random.NextDouble()*cellRange;

			for (var i = 0; i < colorCount; i++)
			{
				var newHue = cellRange*i + cellOffset;

				if (newHue > 1)
					newHue -= 1;

				newHue *= 360;

				var hue = Convert.ToUInt16(newHue);

				var hslColor = new HSL(hue, 0.3f, 0.4f);

				colors.Add(hslColor);
			}

			return colors;
		}
	}
}