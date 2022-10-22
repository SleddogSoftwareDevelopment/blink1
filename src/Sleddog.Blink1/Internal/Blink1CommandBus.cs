using System;
using System.Linq;
using HidApi;
using Sleddog.Blink1.Internal.Interfaces;

namespace Sleddog.Blink1.Internal
{
    internal class Blink1CommandBus : IDisposable
    {
        private readonly DeviceInfo deviceInfo;
        private Device device;
        private readonly byte reportId = 0x01;
        private readonly int reportLength = 9;

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
                var output = device.GetFeatureReport(reportId, 9);

                return query.ToResponseType(output);
            }

            return default;
        }

        private bool WriteData(byte[] data)
        {
            var writeData = new byte[9];

            writeData[0] = reportId;

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