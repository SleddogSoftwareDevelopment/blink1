using System;
using System.Collections.Generic;
using System.Linq;
using HidApi;
using Sleddog.Blink1.Internal.Interfaces;

namespace Sleddog.Blink1.Internal
{
    internal class Blink1CommandBus : IDisposable
    {
        private readonly DeviceInfo deviceInfo;
        private Device device;

        public bool IsConnected => device != null;

        public Blink1CommandBus(DeviceInfo deviceInfo)
        {
            this.deviceInfo = deviceInfo;
        }

        internal string ReadSerial()
        {
            var chars = (from o in deviceInfo.SerialNumber where o != 0 select (char)o).ToArray();

            return $"0x{string.Join(string.Empty, chars)}";
        }

        internal bool SendCommand(IBlink1MultiCommand multiCommand)
        {
            if (!IsConnected)
            {
                Connect();
            }

            var commandResults = (from hc in multiCommand.ToHidCommands()
                                  select WriteData(hc))
                .ToList();

            return commandResults.Any(cr => cr == false);
        }

        //internal T SendQuery<T>(IBlink1MultiQuery<T> query) where T : class
        //{
        //    if (!IsConnected)
        //    {
        //        Connect();
        //    }

        //    var responseSegments = new List<byte[]>();

        //    var hidCommands = query.ToHidCommands().ToList();

        //    foreach (var hidCommand in hidCommands)
        //    {
        //        var commandSend = WriteData(hidCommand);

        //        if (commandSend)
        //        {
        //            byte[] responseData;

        //            //if (readData)
        //            //{
        //            //    responseSegments.Add(responseData);
        //            //}
        //        }
        //    }

        //    if (responseSegments.Count == hidCommands.Count)
        //    {
        //        return query.ToResponseType(responseSegments);
        //    }

        //    return default;
        //}

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
                var output = device.GetFeatureReport(Convert.ToByte(1), 8);

                return query.ToResponseType(output);
            }

            return default;
        }

        private bool WriteData(byte[] data)
        {
            var writeData = new byte[8];

            writeData[0] = Convert.ToByte(1);

            var length = Math.Min(data.Length, writeData.Length - 1);

            Array.Copy(data, 0, writeData, 1, length);

            device.SendFeatureReport(writeData);

            return true;
        }

        public void Connect()
        {
            device = deviceInfo.ConnectToDevice();
        }

        public void Dispose()
        {
            device?.Dispose();
        }
    }
}