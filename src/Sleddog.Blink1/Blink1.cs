using System;
using System.Drawing;
using System.Reactive.Linq;
using Sleddog.Blink1.Commands;

namespace Sleddog.Blink1
{
	public class Blink1 : IDisposable
	{
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

		public bool FadeToColor(Color color, TimeSpan fadeTime)
		{
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

		public void Dispose()
		{
			if (commandBus != null)
				commandBus.Dispose();
		}
	}
}