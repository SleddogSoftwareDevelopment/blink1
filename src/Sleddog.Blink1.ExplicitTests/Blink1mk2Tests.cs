using System;
using System.Drawing;
using System.Threading;
using Xunit;

namespace Sleddog.Blink1.ExplicitTests
{
	public class Blink1Mk2Tests : IClassFixture<Blink1Fixture<IBlink1Mk2>>
	{
		private readonly IBlink1Mk2 blink1;

		public Blink1Mk2Tests(Blink1Fixture<IBlink1Mk2> fixture)
		{
			blink1 = fixture.Device;
		}

		[RequireBlink1Mk2Hardware]
		public void FadeTopToColor()
		{
			var fadeDuration = TimeSpan.FromSeconds(2);

			var actual = blink1.Fade(Color.Red, fadeDuration, LEDPosition.Top);

			Thread.Sleep(fadeDuration);

			Assert.True(actual);
		}

		[RequireBlink1Mk2Hardware]
		public void FadeBottomToColor()
		{
			var fadeDuration = TimeSpan.FromSeconds(2);

			var actual = blink1.Fade(Color.Red, fadeDuration, LEDPosition.Bottom);

			Thread.Sleep(fadeDuration);

			Assert.True(actual);
		}

		[RequireBlink1Mk2Hardware]
		public void FadeToColor()
		{
			var fadeDuration = TimeSpan.FromSeconds(2);

			var actual = blink1.Fade(Color.Red, fadeDuration, LEDPosition.Both);

			Thread.Sleep(fadeDuration);

			Assert.True(actual);
		}

		[RequireBlink1Mk2Hardware]
		public void PlayPreset()
		{
			blink1.Play(0, 11, 0);
		}
	}
}