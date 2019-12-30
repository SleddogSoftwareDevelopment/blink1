using System.Drawing;
using Sleddog.Blink1.Colors;
using Xunit;

namespace Sleddog.Blink1.Tests.Colors
{
	public class GammaCorrectorTests
	{
		[Fact]
		public void Decode()
		{
			var expected = Color.FromArgb(255, 50, 100, 200);

			var sut = new GammaCorrector();

			var actual = sut.Decode(Color.FromArgb(255, 7, 33, 149));

			Assert.Equal(expected, actual, new GoodEnoughColorComparer());
		}

		[Fact]
		public void Encode()
		{
			var expected = Color.FromArgb(255, 7, 33, 149);

			var sut = new GammaCorrector();

			var actual = sut.Encode(Color.FromArgb(255, 50, 100, 200));

			Assert.Equal(expected, actual, new GoodEnoughColorComparer());
		}
	}
}