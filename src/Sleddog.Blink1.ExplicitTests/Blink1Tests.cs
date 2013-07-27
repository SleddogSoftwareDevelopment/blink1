using System;
using System.Drawing;
using System.Linq;
using Xunit;

namespace Sleddog.Blink1.ExplicitTests
{
	public class Blink1Tests
	{
		private const string LowestSerialNumber = "0x1A001000";
		private const string HighestSerialNumber = "0x1A002FFF";

		[RequireBlink1Hardware]
		public void ReadSerialReadsValidSerialNumber()
		{
			var connector = new Blink1Connector();

			var blink1 = connector.Scan().First();

			var actual = blink1.SerialNumber;

			Assert.InRange(actual, LowestSerialNumber, HighestSerialNumber);
		}

		[RequireBlink1Hardware]
		public void ReadPresetReturnsValidPreset() {
			var connector = new Blink1Connector();
			var blink1 = connector.Scan().First();

			var actual = blink1.ReadPreset(0);
			bool result = blink1.FadeToPreset(actual);

			Assert.NotNull(actual);
			Assert.True(result);
		}

		[RequireBlink1Hardware]
		public void FadeToPreset() {
			var connector = new Blink1Connector();
			var blink1 = connector.Scan().First();

			var preset = new Blink1Preset(Color.Green, TimeSpan.FromSeconds(1));

			bool result = blink1.FadeToPreset(preset);
			Assert.True(result);
		}
	}
}