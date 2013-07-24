using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reactive.Linq;
using HidLibrary;
using Sleddog.Blink1.Commands;

namespace Sleddog.Blink1
{
	public class Blink1 : IDisposable
	{
		private readonly HidDevice hidDevice;

		public Blink1(HidDevice hidDevice)
		{
			this.hidDevice = hidDevice;
		}

		public bool IsConnected
		{
			get { return hidDevice.IsOpen; }
		}

		public Version Version
		{
			get { return SendQuery(new VersionQuery()); }
		}

		public string SerialNumber
		{
			get { return SendQuery(new ReadSerialQuery()); }
		}

		public void Dispose()
		{
			if (hidDevice != null && hidDevice.IsOpen)
				hidDevice.CloseDevice();
		}

		public bool Blink(Color color, TimeSpan interval, ushort times)
		{
			var timeOnInMilliseconds = Math.Min(interval.TotalMilliseconds/4, 250);

			var onTime = TimeSpan.FromMilliseconds(timeOnInMilliseconds);

			var x = Observable.Timer(TimeSpan.Zero, interval).TakeWhile(count => count < times).Select(_ => color);
			var y = Observable.Timer(onTime, interval).TakeWhile(count => count < times).Select(_ => Color.Black);

			x.Merge(y).Subscribe(c => SendCommand(new SetColorCommand(c)));

			return true;
		}

		public bool SetColor(Color color)
		{
			return SendCommand(new SetColorCommand(color));
		}

		public bool FadeToColor(Color color, TimeSpan fadeTime)
		{
			return SendCommand(new FadeToColorCommand(color, fadeTime));
		}

		public bool ShowColor(Color color, TimeSpan visibleTime)
		{
			var timer = ObservableExt.TimerMaxTick(1, TimeSpan.Zero, visibleTime);

			var colors = new[] {color, Color.Black}.ToObservable();

			colors.Zip(timer, (c, t) => new {Color = c, Count = t})
				//		.TakeWhile(x => x.Count <= 1)
				.Subscribe(item => SendCommand(new SetColorCommand(item.Color)), () => Debug.WriteLine("Completed ShowColor"));

			return true;
		}

		internal bool SendCommand(IBlink1MultiCommand multiCommand)
		{
			if (!IsConnected)
				Connect();

			var commandResults = (from hc in multiCommand.ToHidCommands()
			                      select hidDevice.WriteFeatureData(hc)).ToList();

			return commandResults.Any(cr => cr == false);
		}

		internal T SendQuery<T>(IBlink1MultiQuery<T> query) where T : class
		{
			if (!IsConnected)
				Connect();

			var responseSegments = new List<byte[]>();

			var hidCommands = query.ToHidCommands().ToList();

			foreach (var hidCommand in hidCommands)
			{
				var commandSend = hidDevice.WriteFeatureData(hidCommand);

				if (commandSend)
				{
					byte[] responseData;

					var readData = hidDevice.ReadFeatureData(out responseData, Convert.ToByte(1));

					if (readData)
						responseSegments.Add(responseData);
				}
			}

			if (responseSegments.Count == hidCommands.Count())
				return query.ToResponseType(responseSegments);

			return default(T);
		}

		internal bool SendCommand(IBlink1Command command)
		{
			if (!IsConnected)
				Connect();

			var commandSend = hidDevice.WriteFeatureData(command.ToHidCommand());

			return commandSend;
		}

		internal T SendQuery<T>(IBlink1Query<T> query) where T : class
		{
			if (!IsConnected)
				Connect();

			var commandSend = hidDevice.WriteFeatureData(query.ToHidCommand());

			if (commandSend)
			{
				byte[] responseData;

				var readData = hidDevice.ReadFeatureData(out responseData, Convert.ToByte(1));

				if (readData)
					return query.ToResponseType(responseData);
			}

			return default(T);
		}

		public void Connect()
		{
			hidDevice.OpenDevice();
		}
	}
}