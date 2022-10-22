using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HidApi;
using Sleddog.Blink1.Colors;
using Sleddog.Blink1.Internal;

namespace Sleddog.Blink1
{
    public static class Blink1Connector
    {
        private const int VendorId = 0x27B8;
        private const int ProductId = 0x01ED;

        private static readonly Dictionary<byte, DeviceType> deviceTypeMap = new Dictionary<byte, DeviceType>
        {
            {0x31, DeviceType.Blink1},
            {0x32, DeviceType.Blink1Mk2}
        };

        public static IBlink1 Connect(string serial)
        {
            var serialToFind = serial.StartsWith("0x") ? serial : $"0x{serial}";

            var deviceInfos = ListBlink1Devices();

            if (deviceInfos.Any())
            {
                foreach (var deviceInfo in deviceInfos)
                {
                    var deviceData = IdentityDevice(deviceInfo);

                    if (deviceData.Item1.Equals(serialToFind, StringComparison.InvariantCultureIgnoreCase))
                    {
                        if (deviceData.Item2 == DeviceType.Blink1)
                        {
                            return new Blink1(new Blink1CommandBus(deviceInfo));
                        }

                        return new Blink1Mk2(new Blink1CommandBus(deviceInfo));
                    }
                }
            }

            return null;
        }

        public static IEnumerable<IBlink1> Scan()
        {
            var devices = ListBlink1Devices();

            if (devices.Any())
            {
                var deviceList = IdentifyDevices(devices).ToArray();

                foreach (var device in deviceList)
                {
                    if (device.Item1 == DeviceType.Blink1)
                    {
                        yield return new Blink1(new Blink1CommandBus(device.Item2));
                    }
                    else
                    {
                        yield return new Blink1Mk2(new Blink1CommandBus(device.Item2));
                    }
                }
            }
        }

        public static IEnumerable<Blink1Identifier> Identify(TimeSpan identifyTime)
        {
            var colorGenerator = new ColorGenerator();

            var blinks = Scan().ToList();

            var colors = colorGenerator.GenerateColors(blinks.Count);

            var blink1Identifiers = (from b in blinks
                                     from c in colors
                                     select new Blink1Identifier(b, c)).ToList();

            Parallel.ForEach(blink1Identifiers, bi =>
            {
                var blink1 = bi.Blink1;

                blink1.Show(bi.Color, identifyTime);
            });

            return blink1Identifiers;
        }

        private static Tuple<string, DeviceType> IdentityDevice(DeviceInfo deviceInfo)
        {
            var chars = (from o in deviceInfo.SerialNumber where o != 0 select (char)o).ToArray();

            var deviceType = DetermineDeviceType((byte)deviceInfo.SerialNumber[0]);

            var serialNumber = $"0x{string.Join(string.Empty, chars)}";

            return Tuple.Create(serialNumber, deviceType);
        }

        private static DeviceInfo[] ListBlink1Devices()
        {
            var devices = Hid.Enumerate(VendorId, ProductId);

            return devices as DeviceInfo[] ?? devices.ToArray();
        }

        private static IEnumerable<Tuple<DeviceType, DeviceInfo>> IdentifyDevices(IEnumerable<DeviceInfo> deviceInfos)
        {
            foreach (var deviceInfo in deviceInfos)
            {
                var significantByte = (byte)deviceInfo.SerialNumber[0];

                var deviceType = DetermineDeviceType(significantByte);

                yield return Tuple.Create(deviceType, deviceInfo);
            }
        }

        private static DeviceType DetermineDeviceType(byte b)
        {
            if (deviceTypeMap.ContainsKey(b))
            {
                return deviceTypeMap[b];
            }

            /*
			 * Default to mk2 in the case the specific device haven't been found
			 * This ensures future versions should work as they'll be 1 model backwards compatible
			 */
            return DeviceType.Blink1Mk2;
        }

        private enum DeviceType
        {
            Blink1,
            Blink1Mk2
        }
    }
}