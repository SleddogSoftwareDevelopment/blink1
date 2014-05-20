using System.Linq;
using HidLibrary;

namespace Sleddog.Blink1.ExplicitTests
{
    public sealed class RequireNoBlink1HardwareAttribute : ExplicitFactAttribute
    {
        private const int VendorId = 0x27B8;
        private const int ProductId = 0x01ED;

        public RequireNoBlink1HardwareAttribute()
        {
            var hidDevices = HidDevices.Enumerate(VendorId, ProductId);

            var devices = hidDevices as HidDevice[] ?? hidDevices.ToArray();

            if (devices.Any())
            {
                Skip = "Blink1 devices connected";
            }
        }
    }
}