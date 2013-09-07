﻿using System;
using System.Drawing;
using System.Threading;
using Xunit;

namespace Sleddog.Blink1.ExplicitTests
{
	public class Blink1Tests : IUseFixture<Blink1Fixture>
	{
		private const string LowestSerialNumber = "0x1A001000";
		private const string HighestSerialNumber = "0x1A002FFF";

		private Blink1 blink1;

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

			Assert.NotNull(actual);
		}

		[RequireBlink1Hardware]
		public void SavePresetWritesToDevice()
		{
			var expected = new Blink1Preset(Color.DarkSlateGray, TimeSpan.FromSeconds(1.5));

			blink1.SavePreset(expected, 0);

			var actual = blink1.ReadPreset(0);

			Assert.Equal(expected, actual);
		}

		[RequireBlink1Hardware]
		public void SetColor()
		{
			var actual = blink1.SetColor(Color.Blue);

			Assert.True(actual);
		}

		[RequireBlink1Hardware]
		public void ShowColor()
		{
			var showColorTime = TimeSpan.FromSeconds(2);

			var actual = blink1.ShowColor(Color.Chartreuse, showColorTime);

			Thread.Sleep(showColorTime);

			Assert.True(actual);
		}

		[RequireBlink1Hardware]
		public void FadeToColor()
		{
			var fadeDuration = TimeSpan.FromSeconds(2);

			var actual = blink1.FadeToColor(Color.Red, fadeDuration);

			Thread.Sleep(fadeDuration);

			Assert.True(actual);
		}

		public void SetFixture(Blink1Fixture data)
		{
			blink1 = data.Device;
		}
	}
}