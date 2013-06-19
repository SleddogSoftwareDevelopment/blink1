using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using HidLibrary;

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
			var blinks = Scan();

			return Enumerable.Empty<Blink1Identifier>();
		}

		private IEnumerable<Color> GenerateColors(int numberOfColors)
		{
			return null;
		}
	}
}