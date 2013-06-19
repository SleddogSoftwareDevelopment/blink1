using System;
using System.Drawing;
using Ploeh.AutoFixture.Xunit;
using Sleddog.Blink1.Colors;
using Xunit;
using Xunit.Extensions;

namespace Sleddog.Blink1.Tests.Colors
{
	public class HSLTests
	{
		[Theory]
		[InlineData((ushort) 361, (ushort) 0, (ushort) 0)]
		[InlineData((ushort) 0, (ushort) 101, (ushort) 0)]
		[InlineData((ushort) 0, (ushort) 0, (ushort) 101)]
		public void HSLCtorBoundaryCheck(ushort hue, ushort saturation, ushort luminance)
		{
			Assert.Throws<ArgumentOutOfRangeException>(() => new HSL(hue, saturation, luminance));
		}

		[Theory, AutoData]
		public void ZeroSaturationRendersColorFactoredByLuminance(ushort hue, ushort luminance)
		{
			var hueValue = (ushort) (hue%360);
			var luminanceValue = (ushort) (luminance%100);

			var hsl = new HSL(hueValue, 0, luminanceValue);

			Color actual = hsl;

			var colorValue = (float) luminanceValue/100*255;

			var expectedColorValue = Convert.ToInt32(colorValue);

			var expected = Color.FromArgb(expectedColorValue, expectedColorValue, expectedColorValue);

			Assert.Equal(expected, actual);
		}
	}
}