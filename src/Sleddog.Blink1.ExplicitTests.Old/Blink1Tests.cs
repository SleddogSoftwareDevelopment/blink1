using System;
using System.Drawing;
using System.Threading;
using Xunit;

namespace Sleddog.Blink1.ExplicitTests
{
    public class Blink1Tests : IClassFixture<Blink1Fixture>
    {
        private const string LowestSerialNumber = "0x1A001000";
        private const string HighestSerialNumber = "0x1A002FFF";

        private readonly IBlink1 blink1;

        public Blink1Tests(Blink1Fixture data)
        {
            blink1 = data.Device;
        }

        [RequireBlink1Hardware]
        public void SetAllPatterns()
        {
            blink1.Save(new Blink1Preset(Color.Cyan, TimeSpan.FromSeconds(5)), 0);
            blink1.Save(new Blink1Preset(Color.DarkCyan, TimeSpan.FromSeconds(5)), 1);
            blink1.Save(new Blink1Preset(Color.CadetBlue, TimeSpan.FromSeconds(5)), 2);
            blink1.Save(new Blink1Preset(Color.SteelBlue, TimeSpan.FromSeconds(5)), 3);
            blink1.Save(new Blink1Preset(Color.DodgerBlue, TimeSpan.FromSeconds(5)), 4);
            blink1.Save(new Blink1Preset(Color.MediumBlue, TimeSpan.FromSeconds(5)), 5);
            blink1.Save(new Blink1Preset(Color.DarkBlue, TimeSpan.FromSeconds(5)), 6);
            blink1.Save(new Blink1Preset(Color.Green, TimeSpan.FromSeconds(5)), 7);
            blink1.Save(new Blink1Preset(Color.SeaGreen, TimeSpan.FromSeconds(5)), 8);
            blink1.Save(new Blink1Preset(Color.MediumSeaGreen, TimeSpan.FromSeconds(5)), 9);
            blink1.Save(new Blink1Preset(Color.SpringGreen, TimeSpan.FromSeconds(5)), 10);
            blink1.Save(new Blink1Preset(Color.LightGreen, TimeSpan.FromSeconds(5)), 11);
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

            Assert.NotNull(actual);
        }

        [RequireBlink1Hardware]
        public void SavePresetWritesToDevice()
        {
            var expected = new Blink1Preset(Color.DarkSlateGray, TimeSpan.FromSeconds(1.5));

            blink1.EnableGamma = false;

            blink1.Save(expected, 0);

            var actual = blink1.ReadPreset(0);

            Assert.Equal(expected, actual);
        }

        [RequireBlink1Hardware]
        public void SetColor()
        {
            var actual = blink1.Set(Color.Blue);

            Assert.True(actual);
        }

        [RequireBlink1Hardware]
        public void ShowColor()
        {
            var showColorTime = TimeSpan.FromSeconds(2);

            var actual = blink1.Show(Color.Chartreuse, showColorTime);

            Thread.Sleep(showColorTime);

            Assert.True(actual);
        }

        [RequireBlink1Hardware]
        public void FadeToColor()
        {
            var fadeDuration = TimeSpan.FromSeconds(2);

            var actual = blink1.Fade(Color.Red, fadeDuration);

            Thread.Sleep(fadeDuration);

            Assert.True(actual);
        }

        [RequireBlink1Hardware]
        public void SetPreset0AndPlayIt()
        {
            var presetDuration = TimeSpan.FromSeconds(2);

            var preset = new Blink1Preset(Color.DarkGoldenrod, presetDuration);

            blink1.Save(preset, 0);

            blink1.Play(0);

            Thread.Sleep(presetDuration);

            blink1.Pause();
        }

        [RequireBlink1Hardware]
        public void PlayPreset()
        {
            blink1.Play(0);

            Thread.Sleep(TimeSpan.FromSeconds(5));

            blink1.Pause();
        }

        [RequireBlink1Hardware]
        public void TurnOff()
        {
            blink1.Set(Color.Black);
        }

        [RequireBlink1Hardware]
        public void EnableInactivityMode()
        {
            blink1.EnableInactivityMode(TimeSpan.FromMilliseconds(50));

            Thread.Sleep(TimeSpan.FromMilliseconds(150));

            blink1.DisableInactivityMode();
        }
    }
}