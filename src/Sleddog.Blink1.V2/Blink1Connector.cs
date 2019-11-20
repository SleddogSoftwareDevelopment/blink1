using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HidSharp;
using Sleddog.Blink1.V2.Colors;
using Sleddog.Blink1.V2.Internal;

namespace Sleddog.Blink1.V2
{
	public class Blink1Connector
	{
		public static IBlink1 Connect(string serial)
		{
			var serialToFind = serial.StartsWith("0x") ? serial : $"0x{serial}";

			var devices = ListBlink1Devices();

			if (devices.Any())
			{
				foreach (var device in devices)
				{
					var deviceData = IdentityDevice(device);

					if (deviceData.Item1.Equals(serialToFind, StringComparison.InvariantCultureIgnoreCase))
					{
						if (deviceData.Item2 == DeviceType.Blink1)
						{
							return new Blink1(new Blink1CommandBus(device));
						}

						return new Blink1Mk2(new Blink1CommandBus(device));
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

		private static Tuple<string, DeviceType> IdentityDevice(HidDevice device)
		{
			var serialNumber = device.GetSerialNumber();
			//TODO: Fix device determination from string
			var deviceType = DetermineDeviceType(0);

			return Tuple.Create(serialNumber, deviceType);
		}

		private static IEnumerable<HidDevice> ListBlink1Devices()
		{
			var deviceList = new FilteredDeviceList();

			return deviceList.GetHidDevices(Constants.VendorId, Constants.ProductId);
		}

		private static IEnumerable<Tuple<DeviceType, HidDevice>> IdentifyDevices(IEnumerable<HidDevice> devices)
		{
			foreach (var device in devices)
			{
				var didRead = device.GetSerialNumber();

				if (!string.IsNullOrWhiteSpace(didRead))
				{
					//TODO: Fix device determination from string
					var deviceType = DetermineDeviceType(0);

					yield return Tuple.Create(deviceType, device);
				}
			}
		}

		private static DeviceType DetermineDeviceType(byte b)
		{
			// 0x31 == blink1, 0x32 == blink1mk2, 

			if (b != 0x31 && b != 0x32)
			{
				throw new InvalidOperationException("Unhandled Blink1 device inserted");
			}

			return b == 0x31 ? DeviceType.Blink1 : DeviceType.Blink1Mk2;
		}

		private enum DeviceType
		{
			Blink1,
			Blink1Mk2
		}
	}
}