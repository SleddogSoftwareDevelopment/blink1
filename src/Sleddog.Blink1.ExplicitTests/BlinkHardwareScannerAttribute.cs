using System.Linq;
using HidSharp;

namespace Sleddog.Blink1.ExplicitTests
{
	public abstract class BlinkHardwareScannerAttribute : ExplicitFactAttribute
	{
		protected readonly HidDevice[] Devices;
		private const int VendorId = 0x27B8;
		private const int ProductId = 0x01ED;

		protected BlinkHardwareScannerAttribute()
		{
			Devices =  DeviceList.Local.GetHidDevices(VendorId, ProductId).ToArray();
		}
	}
}