using HidApi;
using Xunit;

namespace Sleddog.Blink1.ExplicitTests
{
    public abstract class BlinkHardwareScannerAttribute : FactAttribute
    {
        private const int VendorId = 0x27B8;
        private const int ProductId = 0x01ED;

        protected IEnumerable<DeviceInfo> devices;

        protected BlinkHardwareScannerAttribute()
        {
            try
            {
            devices = Hid.Enumerate(VendorId, ProductId);
            }
            catch
            {
                Skip = "Failed to load HidApi";
            }
        }
    }
}
