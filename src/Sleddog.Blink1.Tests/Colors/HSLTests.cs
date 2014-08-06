using System;
using System.Collections.Generic;
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
        [InlineData((ushort) 361, 0, 0)]
        [InlineData((ushort) 0, 1.1f, 0)]
        [InlineData((ushort) 0, 0, 1.1f)]
        public void HSLCtorBoundaryCheck(ushort hue, float saturation, float luminance)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new HSL(hue, saturation, luminance));
        }

        [Theory, PropertyData("HSL2RGB")]
        public void HSLToRGBIsConvertedCorrectly(ushort hue, float saturation, float luminance, Color expected)
        {
            var sut = new HSL(hue, saturation, luminance);

            Color actual = sut;

            Assert.Equal(expected, actual);
        }

        [Theory, AutoData]
        public void ZeroSaturationRendersColorFactoredByLuminance(ushort hue, float luminance)
        {
            var hueValue = (ushort) (hue%360);
            var luminanceValue = luminance%1;

            var hsl = new HSL(hueValue, 0, luminanceValue);

            Color actual = hsl;

            var colorValue = luminanceValue/1*255;

            var expectedColorValue = Convert.ToInt32(colorValue);

            var expected = Color.FromArgb(expectedColorValue, expectedColorValue, expectedColorValue);

            Assert.Equal(expected, actual);
        }

        public static IEnumerable<object[]> HSL2RGB
        {
            get
            {
                return new[]
                {
                    new object[] {(ushort) 0, 0, 1, Color.FromArgb(255, 255, 255)},
                    new object[] {(ushort) 0, 0, 0.502f, Color.FromArgb(128, 128, 128)},
                    new object[] {(ushort) 0, 0, 0, Color.FromArgb(0, 0, 0)},
                    new object[] {(ushort) 0, 1, 0.5f, Color.FromArgb(255, 0, 0)},
                    new object[] {(ushort) 120, 1, 0.5f, Color.FromArgb(0, 255, 0)},
                    new object[] {(ushort) 240, 1, 0.5f, Color.FromArgb(0, 0, 255)},
                    new object[] {(ushort) 284, 0.807f, 0.224f, Color.FromArgb(78, 11, 103)},
                    new object[] {(ushort) 210, 0.50f, 0.165f, Color.FromArgb(21, 42, 63)}
                };
            }
        }
    }
}