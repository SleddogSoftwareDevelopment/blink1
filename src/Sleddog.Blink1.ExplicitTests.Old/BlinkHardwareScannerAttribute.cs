using System.Linq;
using HidLibrary;

namespace Sleddog.Blink1.ExplicitTests
{
    public abstract class BlinkHardwareScannerAttribute : ExplicitFactAttribute
    {
        private const int VendorId = 0x27B8;
        private const int ProductId = 0x01ED;

        protected readonly HidDevice[] Devices;

        protected BlinkHardwareScannerAttribute()
        {
            Devices = HidDevices.Enumerate(VendorId, ProductId).ToArray();
        }
    }
}