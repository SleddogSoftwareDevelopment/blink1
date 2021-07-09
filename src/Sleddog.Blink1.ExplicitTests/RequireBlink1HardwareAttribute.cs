using System.Linq;
using HidSharp;

namespace Sleddog.Blink1.ExplicitTests
{
	public class RequireBlink1HardwareAttribute : RequireBlinkHardwareAttribute
	{
		public RequireBlink1HardwareAttribute()
		{
			var blink1Devices = (from d in Devices where IsDeviceWithinBlink1Range(d) select d).ToArray();

			if (!blink1Devices.Any())
			{
				Skip = "No Blink1 units connected";
			}
		}

		private bool IsDeviceWithinBlink1Range(HidDevice device)
		{
			var serial = device.GetSerialNumber();

			if (string.IsNullOrWhiteSpace(serial))
			{
				return false;
			}

			return serial[0] == 0x31;
		}
	}
}