using System.Linq;
using HidLibrary;

namespace Sleddog.Blink1.ExplicitTests
{
    public class RequireBlink1HardwareAttribute : RequireBlinkHardwareAttribute
    {
        public RequireBlink1HardwareAttribute()
        {
            var blink1Devices = (from d in devices where IsDeviceWithinBlink1Range(d) select d).ToArray();

            if (!blink1Devices.Any())
            {
                Skip = "No Blink1 units connected";
            }
        }

        private bool IsDeviceWithinBlink1Range(HidDevice device)
        {
            byte[] serialBytes;

            var readSerial = device.ReadSerialNumber(out serialBytes);

            if (!readSerial)
            {
                return false;
            }

            return serialBytes[0] == 0x31;
        }
    }
}