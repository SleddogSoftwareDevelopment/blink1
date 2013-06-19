using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
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

			Debug.WriteLine(this.hidDevice.DevicePath);
			Debug.WriteLine(ReadSerial());
		}

		public bool IsConnected
		{
			get { return hidDevice.IsOpen; }
		}

		public Version Version
		{
			get { return SendQuery(new VersionQuery()); }
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
			var offTime = interval.Subtract(onTime);

			Debug.WriteLine("OnTime: {0}; OffTime: {1}", onTime.TotalMilliseconds, offTime.TotalMilliseconds);

			var blackoutCommand = new SetColorCommand(Color.Black);
			var flashCommand = new SetColorCommand(color);

			var count = 0;

			while (count < times)
			{
				SendCommand(flashCommand);

				Thread.Sleep(onTime);

				SendCommand(blackoutCommand);

				if (count < times - 1)
					Thread.Sleep(offTime);

				count++;
			}

			return true;
		}

		public string ReadSerial()
		{
			byte eepromSerialAddress = 2;

			var command = new[] {Convert.ToByte(1), (byte) Blink1Commands.EEPROMRead};

			var serialBytes = new byte[4];

			for (var i = 0; i < 4; i++)
			{
				var serialPartCommand = command.Concat(new[] {eepromSerialAddress++}).ToArray();

				var written = hidDevice.WriteFeatureData(serialPartCommand);

				if (written)
				{
					byte[] outputData;

					var read = hidDevice.ReadFeatureData(out outputData, Convert.ToByte(1));

					if (read)
						serialBytes[i] = outputData[3];
				}
			}

			var serialChars = new List<char>();

			foreach (var b in serialBytes)
			{
				var firstChar = b >> 4;
				var secondChar = b;

				serialChars.Add((char) ToHex(firstChar));
				serialChars.Add((char) ToHex(secondChar));
			}

			return string.Format("0x{0}", string.Join(string.Empty, serialChars));
		}

		private int ToHex(int inputValue)
		{
			var charValue = inputValue & 0x0F;

			return (charValue <= 9) ? (charValue + '0') : (charValue - 10 + 'A');
		}

		public bool SetColor(Color color)
		{
			return SendCommand(new SetColorCommand(color));
		}

		public bool FadeToColor(Color color, TimeSpan fadeTime)
		{
			return SendCommand(new FadeToColorCommand(color, fadeTime));
		}

		internal bool SendCommand(IBlink1Command command)
		{
			if (!IsConnected)
				Connect();

			var commandSend = hidDevice.WriteFeatureData(command.ToByteArray());

			return commandSend;
		}

		internal T SendQuery<T>(IBlink1Query<T> query) where T : class
		{
			if (!IsConnected)
				Connect();

			var commandSend = hidDevice.WriteFeatureData(query.ToByteArray());

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