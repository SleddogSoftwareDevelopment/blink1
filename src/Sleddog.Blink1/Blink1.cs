using System;
using System.Drawing;
using System.Reactive.Linq;
using Sleddog.Blink1.Commands;

namespace Sleddog.Blink1
{
    public class Blink1 : IDisposable, IBlink1
    {
        private const ushort NumberOfPresets = 12;

        private readonly Blink1CommandBus commandBus;

        public Version Version
        {
            get { return commandBus.SendQuery(new VersionQuery()); }
        }

        public string SerialNumber
        {
            get { return commandBus.SendQuery(new ReadSerialQuery()); }
        }

        internal Blink1(Blink1CommandBus commandBus)
        {
            this.commandBus = commandBus;
        }

        public bool Blink(Color color, TimeSpan interval, ushort times)
        {
            var timeOnInMilliseconds = Math.Min(interval.TotalMilliseconds/4, 250);

            var onTime = TimeSpan.FromMilliseconds(timeOnInMilliseconds);

            var x = Observable.Timer(TimeSpan.Zero, interval).TakeWhile(count => count < times).Select(_ => color);
            var y = Observable.Timer(onTime, interval).TakeWhile(count => count < times).Select(_ => Color.Black);

            x.Merge(y).Subscribe(c => commandBus.SendCommand(new SetColorCommand(c)));

            return true;
        }

        public bool SetColor(Color color)
        {
            return commandBus.SendCommand(new SetColorCommand(color));
        }

        public bool FadeToColor(Color color, TimeSpan fadeDuration)
        {
            var fadeTime = new Blink1Duration(fadeDuration);

            return commandBus.SendCommand(new FadeToColorCommand(color, fadeTime));
        }

        public bool ShowColor(Color color, TimeSpan visibleTime)
        {
            var timer = ObservableExt.TimerMaxTick(1, TimeSpan.Zero, visibleTime);

            var colors = new[] {color, Color.Black}.ToObservable();

            colors.Zip(timer, (c, t) => new {Color = c, Count = t})
                .Subscribe(item => commandBus.SendCommand(new SetColorCommand(item.Color)));

            return true;
        }

        public bool SavePreset(Blink1Preset preset, ushort position)
        {
            if (position < NumberOfPresets)
            {
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

        public bool PlaybackPresets(ushort startPosition)
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

        public bool PausePresets()
        {
            return commandBus.SendCommand(new StopPresetCommand());
        }

        public bool EnableInactivityMode(TimeSpan waitDuration)
        {
            var waitTime = new Blink1Duration(waitDuration);

            return commandBus.SendCommand(new EnableInactivityModeCommand(waitTime));
        }

        public bool DisableInactivityMode()
        {
            return commandBus.SendCommand(new DisableInactivityModeCommand());
        }

	    public void TurnOff()
	    {
	        SetColor(Color.Black);
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