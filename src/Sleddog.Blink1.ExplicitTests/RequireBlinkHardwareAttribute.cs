using HidApi;

namespace Sleddog.Blink1.ExplicitTests
{
    public class RequireBlinkHardwareAttribute : BlinkHardwareScannerAttribute
    {
        public RequireBlinkHardwareAttribute()
        {
            if (!devices.Any())
                Skip = "No Blink1 devices connected";
        }
    }

}
