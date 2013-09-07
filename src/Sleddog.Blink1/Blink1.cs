using System;
using System.Drawing;
using System.Reactive.Linq;
using Sleddog.Blink1.Commands;

namespace Sleddog.Blink1
{
	public class Blink1 : IDisposable
	{
		public const int NumberOfPresets = 12;

		private readonly Blink1CommandBus commandBus;

		public Blink1(Blink1CommandBus commandBus)
		{
			this.commandBus = commandBus;
		}

		public Version Version
		{
			get { return commandBus.SendQuery(new VersionQuery()); }
		}

		public string SerialNumber
		{
			get { return commandBus.SendQuery(new ReadSerialQuery()); }
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

		public bool PlaybackPresets(ushort startPosition)
		{
			return commandBus.SendCommand(new PresetControlCommand(true, startPosition));
		}

		public bool PausePresets()
		{
			return commandBus.SendCommand(new PresetControlCommand(false));
		}

		public bool FadeToPreset(Blink1Preset preset)
		{
			return FadeToColor(preset.Color, preset.Duration);
		}

		public Blink1Preset ReadPreset(ushort position)
		{
			return commandBus.SendQuery(new ReadPresetQuery(position));
		}

		public bool SavePreset(Blink1Preset preset, ushort position)
		{
			return commandBus.SendCommand(new SetPresetCommand(preset, position));
		}

		public void Dispose()
		{
			if (commandBus != null)
				commandBus.Dispose();
		}
	}
}