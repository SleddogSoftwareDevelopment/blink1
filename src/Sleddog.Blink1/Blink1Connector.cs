using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HidLibrary;
using Sleddog.Blink1.Colors;
using Sleddog.Blink1.Internal;

namespace Sleddog.Blink1
{
    public static class Blink1Connector
    {
        private const int VendorId = 0x27B8;
        private const int ProductId = 0x01ED;

        public static IEnumerable<IBlink1> Scan()
        {
            var hidDevices = HidDevices.Enumerate(VendorId, ProductId);

            var devices = hidDevices as HidDevice[] ?? hidDevices.ToArray();

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

        private static IEnumerable<Tuple<DeviceType, HidDevice>> IdentifyDevices(IEnumerable<HidDevice> devices)
        {
            foreach (var device in devices)
            {
                byte[] serialBytes;

                var didRead = device.ReadSerialNumber(out serialBytes);

                if (didRead)
                {
                    // 0x31 == blink1, 0x32 == blink1mk2, 
                    var deviceType = serialBytes[0] <= 0x31 ? DeviceType.Blink1 : DeviceType.Blink1mk2;

                    yield return Tuple.Create(deviceType, device);
                }
            }
        }

        public static IEnumerable<Blink1Identifier> Identify(TimeSpan identifyTime)
        {
            var colorGenerator = new ColorGenerator();

            var blinks = Scan().ToList();

            var colors = colorGenerator.GenerateColors(blinks.Count());

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

        private enum DeviceType
        {
            Blink1,
            Blink1mk2
        }
    }
}