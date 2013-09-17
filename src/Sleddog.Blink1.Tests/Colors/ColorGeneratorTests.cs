using System.Linq;
using Ploeh.AutoFixture.Xunit;
using Sleddog.Blink1.Colors;
using Xunit;
using Xunit.Extensions;

namespace Sleddog.Blink1.Tests.Colors
{
	public class ColorGeneratorTests
	{
		[Theory, AutoData]
		public void GenerateColorsReturnsTheGivenCountOfColors(int numberOfColors)
		{
			var sut = new ColorGenerator();

			var colors = sut.GenerateColors(numberOfColors);

			var expected = numberOfColors;
			var actual = colors.Count;

			Assert.Equal(expected, actual);
		}

		[Theory, AutoData]
		public void GeneratedColorsAreDifferent(int numberOfColors)
		{
			var sut = new ColorGenerator();

			var expected = sut.GenerateColors(numberOfColors);

			var actual = expected.Distinct();

			Assert.Equal(expected, actual);
		}
	}
}