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

        private const int mk2Cutoff = 0x20000000;

        public static IEnumerable<IBlink1> Scan()
        {
            var hidDevices = HidDevices.Enumerate(VendorId, ProductId);

            var devices = hidDevices as HidDevice[] ?? hidDevices.ToArray();

            if (devices.Any())
            {
                return devices.Select(device => new Blink1(new Blink1CommandBus(device)));
            }

            return Enumerable.Empty<IBlink1>();
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
    }
}