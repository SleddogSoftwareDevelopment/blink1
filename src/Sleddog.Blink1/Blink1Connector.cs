using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using HidLibrary;
using Sleddog.Blink1.Colors;

namespace Sleddog.Blink1
{
	public class Blink1Connector
	{
		private const int VendorId = 0x27B8;
		private const int ProductId = 0x01ED;

		public IEnumerable<Blink1> Scan()
		{
			var hidDevices = HidDevices.Enumerate(VendorId, ProductId);

			var devices = hidDevices as HidDevice[] ?? hidDevices.ToArray();

			if (devices.Any())
				return devices.Select(device => new Blink1(device));

			return Enumerable.Empty<Blink1>();
		}

		public IEnumerable<Blink1Identifier> Identify(TimeSpan identifyTime)
		{
			var colorGenerator = new ColorGenerator();

			var blinks = Scan().ToList();

			var colors = colorGenerator.GenerateColors(blinks.Count());

			var blink1Identifiers = (from b in blinks
			                         from c in colors
			                         select new Blink1Identifier(b, c)).ToList();

			Parallel.ForEach(blink1Identifiers, bi => {
				                                    var blink1 = bi.Blink1;

				                                    blink1.SetColor(bi.Color);

				                                    blink1.FadeToColor(Color.Black, identifyTime);
			                                    });

			return blink1Identifiers;
		}
	}
}