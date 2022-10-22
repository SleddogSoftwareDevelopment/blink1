using HidApi;

namespace Sleddog.Blink1.ExplicitTests
{
    public class RequireBlink1Mk2HardwareAttribute : RequireBlinkHardwareAttribute
    {
        public RequireBlink1Mk2HardwareAttribute()
        {
            var blink1Devices = (from d in devices where IsDeviceWithinBlink1mk2Range(d) select d).ToArray();

            if (!blink1Devices.Any())
            {
                Skip = "No Blink1mk2 units connected";
            }
        }

        private bool IsDeviceWithinBlink1mk2Range(DeviceInfo deviceInfo)
        {
            return deviceInfo.SerialNumber[0] >= 0x32;
        }
    }
}
