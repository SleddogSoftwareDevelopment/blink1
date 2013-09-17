using System.Linq;
using FluentAssertions;
using Sleddog.Blink1.Colors;
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

			actual.Should().Be(expected);
		}

		[Theory, AutoData]
		public void GeneratedColorsAreDifferent(int numberOfColors)
		{
			var sut = new ColorGenerator();

			var expected = sut.GenerateColors(numberOfColors);

			var actual = expected.Distinct();

			actual.Should().Be(expected);
		}
	}
}