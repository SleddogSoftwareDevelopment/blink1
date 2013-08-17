using System;
using System.Drawing;
using System.Linq;
using Xunit;

namespace Sleddog.Blink1.ExplicitTests
{
	public class Blink1Tests : IDisposable
	{
		private const string LowestSerialNumber = "0x1A001000";
		private const string HighestSerialNumber = "0x1A002FFF";

		private readonly Blink1 blink1;

		public Blink1Tests()
		{
			blink1 = new Blink1Connector().Scan().FirstOrDefault();
		}

		[RequireBlink1Hardware]
		public void ReadSerialReadsValidSerialNumber()
		{
			var actual = blink1.SerialNumber;

			Assert.InRange(actual, LowestSerialNumber, HighestSerialNumber);
		}

		[RequireBlink1Hardware]
		public void ReadPresetReturnsValidPreset()
		{
			var actual = blink1.ReadPreset(0);
			var result = blink1.FadeToPreset(actual);

			Assert.NotNull(actual);
			Assert.True(result);
		}

		[RequireBlink1Hardware]
		public void SavePresetWritesToDevice()
		{
			var expected = new Blink1Preset(Color.DarkSlateGray, TimeSpan.FromSeconds(1.5));

			blink1.SavePreset(expected, 0);
			var actual = blink1.ReadPreset(0);
			blink1.FadeToPreset(actual);

			Assert.Equal(expected.Color.R, actual.Color.R);
			Assert.Equal(expected.Color.G, actual.Color.G);
			Assert.Equal(expected.Color.B, actual.Color.B);
			Assert.Equal(expected.Duration, actual.Duration);
		}

		[RequireBlink1Hardware]
		public void FadeToPreset()
		{
			var preset = new Blink1Preset(Color.Green, TimeSpan.FromSeconds(1));

			var result = blink1.FadeToPreset(preset);
			Assert.True(result);
		}

		public void Dispose()
		{
			if (blink1 != null)
				blink1.Dispose();
		}
	}
}