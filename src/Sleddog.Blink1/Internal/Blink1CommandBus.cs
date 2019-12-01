using System;
using HidSharp;
using Sleddog.Blink1.Internal.Interfaces;

namespace Sleddog.Blink1.Internal
{
	internal class Blink1CommandBus : IDisposable
	{
		private readonly HidDevice hidDevice;
		private HidStream hidStream;

		public bool IsConnected { get; private set; }

		public Blink1CommandBus(HidDevice hidDevice)
		{
			this.hidDevice = hidDevice;
		}

		public void Dispose()
		{
			hidStream?.Close();
		}

		internal string ReadSerial()
		{
			return hidDevice.GetSerialNumber();
		}

		internal bool SendCommand(IBlink1MultiCommand multiCommand)
		{
			if (!IsConnected)
			{
				Connect();
			}

			var commandResults = (from hc in multiCommand.ToHidCommands()
			                      select WriteData(hc)
			                     ).ToList();

			return commandResults.Any(cr => cr == false);
		}

		internal T SendQuery<T>(IBlink1MultiQuery<T> query) where T : class
		{
			if (!IsConnected)
			{
				Connect();
			}

			var responseSegments = new List<byte[]>();

			var hidCommands = query.ToHidCommands().ToList();

			foreach (var hidCommand in hidCommands)
			{
				var commandSend = WriteData(hidCommand);

				if (commandSend)
				{
					var responseData = new byte[8];

					responseData[0] = Convert.ToByte(1);

					hidStream.GetFeature(responseData);

					responseSegments.Add(responseData);
				}
			}

			if (responseSegments.Count == hidCommands.Count)
			{
				return query.ToResponseType(responseSegments);
			}

			return default;
		}

		internal bool SendCommand(IBlink1Command command)
		{
			if (!IsConnected)
			{
				Connect();
			}

			var commandSend = WriteData(command.ToHidCommand());

			return commandSend;
		}

		internal T SendQuery<T>(IBlink1Query<T> query) where T : class
		{
			if (!IsConnected)
			{
				Connect();
			}

			var commandSend = WriteData(query.ToHidCommand());

			if (commandSend)
			{
				var responseData = new byte[8];

				responseData[0] = Convert.ToByte(1);

				hidStream.GetFeature(responseData);

				return query.ToResponseType(responseData);
			}

			return default;
		}

		private bool WriteData(byte[] data)
		{
			var writeData = new byte[8];

			writeData[0] = Convert.ToByte(1);

			var length = Math.Min(data.Length, writeData.Length - 1);

			Array.Copy(data, 0, writeData, 1, length);

			hidStream.SetFeature(writeData);

			return true;
		}

		public void Connect()
		{
			hidStream = hidDevice.Open();
			IsConnected = true;
		}
	}
}