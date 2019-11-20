using System;
using System.Drawing;
using System.Reactive.Linq;
using Sleddog.Blink1.Internal;
using Sleddog.Blink1.V2.Colors;
using Sleddog.Blink1.V2.Commands;
using Sleddog.Blink1.V2.Internal;

namespace Sleddog.Blink1.V2
{
    public class Blink1 : IBlink1, IDisposable
    {
        internal readonly Blink1CommandBus CommandBus;
        protected readonly ushort NumberOfPresets;

        internal Blink1(Blink1CommandBus commandBus) : this(commandBus, 12)
        {
        }

        internal Blink1(Blink1CommandBus commandBus, ushort numberOfPresets)
        {
            EnableGamma = true;

            CommandBus = commandBus;
            NumberOfPresets = numberOfPresets;
        }

        public bool EnableGamma { get; set; }

        public Version Version => CommandBus.SendQuery(new VersionQuery());

        public string SerialNumber => CommandBus.ReadSerial();

        public bool Blink(Color inputColor, TimeSpan interval, ushort times)
        {
            var timeOnInMilliseconds = Math.Min(interval.TotalMilliseconds / 4, 250);

            var onTime = TimeSpan.FromMilliseconds(timeOnInMilliseconds);

            var color = ProcessColor(inputColor);

            var x = Observable.Timer(TimeSpan.Zero, interval).TakeWhile(count => count < times).Select(_ => color);
            var y = Observable.Timer(onTime, interval).TakeWhile(count => count < times).Select(_ => Color.Black);

            x.Merge(y).Subscribe(c => CommandBus.SendCommand(new SetColorCommand(c)));

            return true;
        }

        public bool Set(Color inputColor)
        {
            var color = ProcessColor(inputColor);

            return CommandBus.SendCommand(new SetColorCommand(color));
        }

        public bool Fade(Color inputColor, TimeSpan fadeDuration)
        {
            var color = ProcessColor(inputColor);

            return CommandBus.SendCommand(new FadeToColorCommand(color, fadeDuration));
        }

        public bool Show(Color inputColor, TimeSpan visibleTime)
        {
            var timer = ObservableExt.TimerMaxTick(1, TimeSpan.Zero, visibleTime);

            var color = ProcessColor(inputColor);

            var colors = new[] {color, Color.Black}.ToObservable();

            colors.Zip(timer, (c, t) => new {Color = c, Count = t})
                  .Subscribe(item => CommandBus.SendCommand(new SetColorCommand(item.Color)));

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

                    return CommandBus.SendCommand(new SetPresetCommand(correctedPreset, position));
                }

                return CommandBus.SendCommand(new SetPresetCommand(preset, position));
            }

            var message = $"Unable to save a preset outside the upper count ({NumberOfPresets}) of preset slots";

            throw new ArgumentOutOfRangeException(nameof(position), message);
        }

        public Blink1Preset ReadPreset(ushort position)
        {
            if (position < NumberOfPresets)
                return CommandBus.SendQuery(new ReadPresetQuery(position));

            var message = $"Unable to read a preset from position {position} since there is only {NumberOfPresets} preset slots";

            throw new ArgumentOutOfRangeException(nameof(position), message);
        }

        public bool Play(ushort startPosition)
        {
            if (startPosition < NumberOfPresets)
                return CommandBus.SendCommand(new PlayPresetCommand(startPosition));

            var message = $"Unable to play from position {startPosition} since there is only {NumberOfPresets} preset slots";

            throw new ArgumentOutOfRangeException(nameof(startPosition), message);
        }

        public bool Pause()
        {
            return CommandBus.SendCommand(new StopPresetCommand());
        }

        public bool EnableInactivityMode(TimeSpan waitDuration)
        {
            return CommandBus.SendCommand(new EnableInactivityModeCommand(waitDuration));
        }

        public bool DisableInactivityMode()
        {
            return CommandBus.SendCommand(new DisableInactivityModeCommand());
        }

        public void TurnOff()
        {
            Set(Color.Black);
        }

        public void Dispose()
        {
            CommandBus?.Dispose();
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
    }
}