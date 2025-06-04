using System.Drawing;
using Sleddog.Blink1.Colors;
using Xunit;
using AutoFixture.Xunit2;

namespace Sleddog.Blink1.Tests.Colors
{
    public class GammaCorrectorTests
    {
        [Fact]
        public void Encode_Black_ReturnsBlack()
        {
            var corrector = new GammaCorrector();
            var black = Color.FromArgb(0, 0, 0);
            var result = corrector.Encode(black);
            Assert.Equal(black, result);
        }

        [Fact]
        public void Encode_White_ReturnsWhite()
        {
            var corrector = new GammaCorrector();
            var white = Color.FromArgb(255, 255, 255);
            var result = corrector.Encode(white);
            Assert.Equal(white, result);
        }

        [Theory]
        [AutoData]
        public void Encode_Color_ReturnsExpectedGammaCorrectedColor(int r, int g, int b)
        {
            var corrector = new GammaCorrector();
            var color = Color.FromArgb(r, g, b);
            var result = corrector.Encode(color);
            var expected = Color.FromArgb(
                GammaCorrect(color.R, 2.2),
                GammaCorrect(color.G, 2.2),
                GammaCorrect(color.B, 2.2));
            Assert.Equal(expected, result);
        }

        private static int GammaCorrect(int value, double gamma)
        {
            return (int)Math.Round(Math.Pow(value / 255.0, gamma) * 255);
        }
    }
}
