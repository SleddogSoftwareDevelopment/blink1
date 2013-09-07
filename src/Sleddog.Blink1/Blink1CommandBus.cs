using System;
using System.Collections.Generic;
using System.Linq;
using HidLibrary;
using Sleddog.Blink1.Commands;
using Sleddog.Blink1.Interfaces;

namespace Sleddog.Blink1
{
	public class Blink1CommandBus : IDisposable
	{
		private readonly HidDevice hidDevice;

		public bool IsConnected
		{
			get { return hidDevice.IsOpen; }
		}

		public Blink1CommandBus(HidDevice hidDevice)
		{
			this.hidDevice = hidDevice;
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

		public void Dispose()
		{
			if (hidDevice != null && hidDevice.IsOpen)
				hidDevice.CloseDevice();
		}
	}
}