using System;
using System.Drawing;
using System.Reactive.Linq;
using Sleddog.Blink1.Colors;
using Sleddog.Blink1.Commands;
using Sleddog.Blink1.Internal;

namespace Sleddog.Blink1
{
    public class Blink1 : IBlink1, IDisposable
    {
        private const ushort NumberOfPresets = 12;

        internal readonly Blink1CommandBus commandBus;

        public bool EnableGamma { get; set; }

        public Version Version
        {
            get { return commandBus.SendQuery(new VersionQuery()); }
        }

        public string SerialNumber
        {
            get { return commandBus.ReadSerial(); }
        }

        internal Blink1(Blink1CommandBus commandBus)
        {
            EnableGamma = true;

            this.commandBus = commandBus;
        }

        public bool Blink(Color inputColor, TimeSpan interval, ushort times)
        {
            var timeOnInMilliseconds = Math.Min(interval.TotalMilliseconds/4, 250);

            var onTime = TimeSpan.FromMilliseconds(timeOnInMilliseconds);

            var color = ProcessColor(inputColor);

            var x = Observable.Timer(TimeSpan.Zero, interval).TakeWhile(count => count < times).Select(_ => color);
            var y = Observable.Timer(onTime, interval).TakeWhile(count => count < times).Select(_ => Color.Black);

            x.Merge(y).Subscribe(c => commandBus.SendCommand(new SetColorCommand(c)));

            return true;
        }

        public bool Set(Color inputColor)
        {
            var color = ProcessColor(inputColor);

            return commandBus.SendCommand(new SetColorCommand(color));
        }

        public bool Fade(Color inputColor, TimeSpan fadeDuration)
        {
            var color = ProcessColor(inputColor);

            return commandBus.SendCommand(new FadeToColorCommand(color, fadeDuration));
        }

        public bool Show(Color inputColor, TimeSpan visibleTime)
        {
            var timer = ObservableExt.TimerMaxTick(1, TimeSpan.Zero, visibleTime);

            var color = ProcessColor(inputColor);

            var colors = new[] {color, Color.Black}.ToObservable();

            colors.Zip(timer, (c, t) => new {Color = c, Count = t})
                .Subscribe(item => commandBus.SendCommand(new SetColorCommand(item.Color)));

            return true;
        }

        public bool Save(Blink1Preset preset, ushort position)
        {
            if (position < NumberOfPresets)
            {
                if (EnableGamma)
                {
                    var color = ProcessColor(preset.Color);

                    var correctedPreset = new Blink1Preset(color, preset.Duration);

                    return commandBus.SendCommand(new SetPresetCommand(correctedPreset, position));
                }

                return commandBus.SendCommand(new SetPresetCommand(preset, position));
            }

            var message = string.Format("Unable to save a preset outside the upper count ({0}) of preset slots",
                NumberOfPresets);

            throw new ArgumentOutOfRangeException("position", message);
        }

        public Blink1Preset ReadPreset(ushort position)
        {
            if (position < NumberOfPresets)
            {
                return commandBus.SendQuery(new ReadPresetQuery(position));
            }

            var message = string.Format(
                "Unable to read a preset from position {0} since there is only {1} preset slots", position,
                NumberOfPresets);

            throw new ArgumentOutOfRangeException("position", message);
        }

        public bool Play(ushort startPosition)
        {
            if (startPosition < NumberOfPresets)
            {
                return commandBus.SendCommand(new PlayPresetCommand(startPosition));
            }

            var message = string.Format("Unable to play from position {0} since there is only {1} preset slots",
                startPosition,
                NumberOfPresets);

            throw new ArgumentOutOfRangeException("startPosition", message);
        }

        public bool Pause()
        {
            return commandBus.SendCommand(new StopPresetCommand());
        }

        public bool EnableInactivityMode(TimeSpan waitDuration)
        {
            return commandBus.SendCommand(new EnableInactivityModeCommand(waitDuration));
        }

        public bool DisableInactivityMode()
        {
            return commandBus.SendCommand(new DisableInactivityModeCommand());
        }

        public void TurnOff()
        {
            Set(Color.Black);
        }

        private Color ProcessColor(Color inputColor)
        {
            if (EnableGamma)
            {
                var gammaCorrector = new GammaCorrector();

                return gammaCorrector.Encode(inputColor);
            }

            return inputColor;
        }

        public void Dispose()
        {
            if (commandBus != null)
            {
                commandBus.Dispose();
            }
        }
    }
}